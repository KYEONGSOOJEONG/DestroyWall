using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Text")]
    public TextMeshProUGUI destroyCountText;
    public TextMeshProUGUI endDestroyCountText;
    public TextMeshProUGUI bossClearText;
    public TextMeshProUGUI getCoinText;
    public TextMeshProUGUI gameOverText;
    [Header("Button")]
    public Button damageButton;
    public Button speedButton;
    [Header("Music")]
    public AudioClip bossMusic;
    public AudioSource audioSource;
    [Space(10f)]
    public GameObject gameOverPanel;
    [Space(10f)]
    [HideInInspector]
    public float abilityDuration;
    [HideInInspector]
    public bool skillOn;
    [HideInInspector]
    public bool isGameOver;
    [HideInInspector] public float speedBuffValue;
    private int getCoinAtEnd;
    private float coolTime = 8f;
    void Start()
    {
        Application.targetFrameRate = 60;

        

        int selectedAbility = TitleManager.Instance.selectedAbility;
        

        if(selectedAbility == 1)
        {
            damageButton.gameObject.SetActive(true);
            damageButton.onClick.AddListener(() => StartCoroutine(UseDamageSkill(damageButton)));
            
        }
        else if (selectedAbility == 2)
        {
            speedButton.gameObject.SetActive(true);
            speedButton.onClick.AddListener(() => StartCoroutine(UseSpeedBuff(speedButton)));
        }

    }

    private void Awake()
    {
        instance = this;
        skillOn = false;
        isGameOver = false;
        speedBuffValue = 1;
    }

    void LateUpdate()
    {
        UpdateCountText();
    }

    void UpdateCountText()
    {
        destroyCountText.text = "파괴한 개수 : " + Wall.destroyCount.ToString();
        
    }

    IEnumerator UseDamageSkill(Button damageButton)
    {
        // 2배버프 시작
        //버튼 활성 지속시간
        skillOn = true;
        damageButton.interactable = false;
        yield return new WaitForSeconds(abilityDuration);

        //특수 비할성 
        skillOn = false;
        yield return new WaitForSeconds(coolTime);

        damageButton.interactable = true;

    }

    IEnumerator UseSpeedBuff(Button speedButton)
    {
        //딜레이를 절반으로 줄이고 무기 자체의 날라가는 스피드를 더한다 
        speedBuffValue = 2f;
        WpSpawner.instance.SpeedBuffStart();
        speedButton.interactable = false;
        yield return new WaitForSeconds(abilityDuration);

        speedBuffValue = 1;
        WpSpawner.instance.SpeedBuffEnd();
        yield return new WaitForSeconds(coolTime);
        speedButton.interactable = true;
    }
    public void EndGame()
    {
        //플레이어가 받을 코인보상
        int get = HowMuchGet() + TitleManager.Instance.coin;

        PlayerPrefs.SetInt("GetCoin", get);
        PlayerPrefs.Save();
        //파괴회수를 따로 저장하고 타이틀매니저에 전달 저장 
        TitleManager.Instance.destroyCountForSend = Wall.destroyCount;
        Wall.destroyCount = 0;
        TitleManager.Instance.wpCurrentIndex = WpSpawner.instance.CurrentPrefabIndex;

        SceneManager.LoadScene(0);
        //코인정산-게임 결과 성적에 따른 코인지급과 업적버튼 활성화
        //깔끔하게 보려면 타이틀매니저 안에서 하나의 함수를 만들어서 이 세개를 실행시키고 그걸 가져와도 될듯
        
    }

    public void CoinGetText()
    {
        //이 함수는 겜오버가 셋액티브 트루되면 거기서 실행
        endDestroyCountText.text = "파괴한 개수 : " + Wall.destroyCount.ToString();
        bossClearText.text = "격파한 보스 :" +BossMoveMent.bossClered.ToString();
        getCoinText.text = HowMuchGet().ToString();
        if(BossMoveMent.bossClered > 0)
        {
            gameOverText.text = "Clear!";
        }
        else
        {
            gameOverText.text = "Game Over!";
        }
    }

    public int HowMuchGet()
    {
        int a = Wall.destroyCount;
        int getCoinAtEnd;
        
        if(a > 52)
        {
            getCoinAtEnd = 10;
        }
        else if (a  >=45)
        {
            getCoinAtEnd = 7;
        }
        else if (a >= 29)
        {
            getCoinAtEnd = 5;
        }
        else if(a > 17)
        {
            getCoinAtEnd = 3;
        }
        else
        {
            getCoinAtEnd = 2;
        }
        //보스 잡으면 보상 증가
        int b;
        if(BossMoveMent.bossClered > 0 )
        {
             b = 3;
        }
        else
        {
            b = 0;
        }
        return getCoinAtEnd + b;
    }

    public void PlayBossMusic()
    {
        audioSource.clip = bossMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void GameOver()
    {
        //타이틀매니저에서 업적 갱신하는거 ui매니저 start에서 안되면 여기서 실행해볼것
        isGameOver = true;
        audioSource.Stop();
        
        //패널 온 
        gameOverPanel.SetActive(true);
    }
}