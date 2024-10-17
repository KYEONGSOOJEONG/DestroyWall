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
        destroyCountText.text = "�ı��� ���� : " + Wall.destroyCount.ToString();
        
    }

    IEnumerator UseDamageSkill(Button damageButton)
    {
        // 2����� ����
        //��ư Ȱ�� ���ӽð�
        skillOn = true;
        damageButton.interactable = false;
        yield return new WaitForSeconds(abilityDuration);

        //Ư�� ���Ҽ� 
        skillOn = false;
        yield return new WaitForSeconds(coolTime);

        damageButton.interactable = true;

    }

    IEnumerator UseSpeedBuff(Button speedButton)
    {
        //�����̸� �������� ���̰� ���� ��ü�� ���󰡴� ���ǵ带 ���Ѵ� 
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
        //�÷��̾ ���� ���κ���
        int get = HowMuchGet() + TitleManager.Instance.coin;

        PlayerPrefs.SetInt("GetCoin", get);
        PlayerPrefs.Save();
        //�ı�ȸ���� ���� �����ϰ� Ÿ��Ʋ�Ŵ����� ���� ���� 
        TitleManager.Instance.destroyCountForSend = Wall.destroyCount;
        Wall.destroyCount = 0;
        TitleManager.Instance.wpCurrentIndex = WpSpawner.instance.CurrentPrefabIndex;

        SceneManager.LoadScene(0);
        //��������-���� ��� ������ ���� �������ް� ������ư Ȱ��ȭ
        //����ϰ� ������ Ÿ��Ʋ�Ŵ��� �ȿ��� �ϳ��� �Լ��� ���� �� ������ �����Ű�� �װ� �����͵� �ɵ�
        
    }

    public void CoinGetText()
    {
        //�� �Լ��� �׿����� �¾�Ƽ�� Ʈ��Ǹ� �ű⼭ ����
        endDestroyCountText.text = "�ı��� ���� : " + Wall.destroyCount.ToString();
        bossClearText.text = "������ ���� :" +BossMoveMent.bossClered.ToString();
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
        //���� ������ ���� ����
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
        //Ÿ��Ʋ�Ŵ������� ���� �����ϴ°� ui�Ŵ��� start���� �ȵǸ� ���⼭ �����غ���
        isGameOver = true;
        audioSource.Stop();
        
        //�г� �� 
        gameOverPanel.SetActive(true);
    }
}