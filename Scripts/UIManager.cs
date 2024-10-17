using TMPro;
#if UNITY_EDITOR
using UnityEditor.Rendering;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("MainMenu")]
    public GameObject storePanel;
    public GameObject achieveMentPanel;
    [Header("Achievement")]
    public Button[] destroyAchieveMentsBtt;
    public Button[] weapondevelopmentBtt;
    public Button[] usedCoinAchieveMentBtt;
    [Header("StoreButton")]
    public GameObject lockDamageButton;
    public GameObject unLockDamageButton;
    public GameObject lockSpeedButton;
    public GameObject unLockSpeedButton;
    public GameObject[] stackButtons;
    [Header("CoinText")]
    public TextMeshProUGUI coinText;

    private int[] destroyCutline = { 10, 25, 45, 70 };
    private int[] usedCoinCutline = { 0, 54 };
    void Start()
    {
        //buff,skill 초기화
        TitleManager.Instance.ResetData();
        CoinRestore();
        //coin 설정 
        RefreshAchieveMents();
        RefreshStoreButton();
        // 업적 초기화
        WeapoonAchieveCheck();
        //타이틀매니져에 있는 상태값이 true이면 해당버튼들이 true여야한다근데 누르면 다시 트루가 되는데
        //이 다음에 버튼상태유지
        KeepButtonState();
    }

    private void Awake()
    {
        //이렇게 선언되면 매번 오브젝트가 생성될대 bool 값들이 새로 생성될거고 그러면 false를 반환할듯
        //여기 스크립트에서는 오브젝트의 셋액티브 활성화만 고쳐주고 계산자체는 Title에서
    }


    private void WeapoonAchieveCheck()
    {
        for (int i = 0; i < 2; i++)
        {
            if (TitleManager.Instance.wpCurrentIndex > i && !TitleManager.Instance.isdeveloped[i])
            {
                weapondevelopmentBtt[i].interactable = true;
                TitleManager.Instance.isdeveloped[i] = true;
                TitleManager.Instance.wpdevelopClicked[i] = true;
            }
        }
        //완
    }

    private void RefreshAchieveMents()
    {
        //타이틀매니저에서 받아온 bool 값에 따라 여기에 할당하는 오브젝트 들의 interactable값이 설정된다. 타이틀매니저에서는 bool값 만 저장
        int count = TitleManager.Instance.destroyCountForSend;

        for (int i = 0; i < destroyAchieveMentsBtt.Length; i++)
        {
            //받아온 bool값에 따라 버튼 활성화 선택 
            if (count >= destroyCutline[i] && !TitleManager.Instance.clearedAchievements[i])
            {
                destroyAchieveMentsBtt[i].interactable = true;
                TitleManager.Instance.clearedAchievements[i] = true;
                TitleManager.Instance.destroyBttClicked[i] = true;
            }
        }
        //완
    }
    private void CoinRestore()
    {
        coinText.text = TitleManager.Instance.coin.ToString();
        //완
    }

    private void RefreshStoreButton()
    {
        // 만약 타이틀매니저에 있는 불값이 트루라면 그 해당 버튼이 셋액티브 트루여야한다. 이버튼은 상점이 열릴때 하면된다 
        if (TitleManager.Instance.buyDamageBuff)
        {
            lockDamageButton.SetActive(false);
            unLockDamageButton.SetActive(true);
        }

        if (TitleManager.Instance.buySpeedBuff)
        {
            lockSpeedButton.SetActive(false);
            unLockSpeedButton.SetActive(true);
        }
        for (int i = 0; i + 1 < stackButtons.Length; i++)
        {
            if (TitleManager.Instance.buyStackBuff[i] && !TitleManager.Instance.buyStackBuff[i + 1])
            {
                stackButtons[0].SetActive(false);
                stackButtons[i + 1].SetActive(true);
                break;
            }
        }
    }
    public void BuyStackBuff(int cost)
    {
        if (!TitleManager.Instance.BuyStackBuff(cost))
        {
            return;
        }
        CoinRestore();

        //3개 할당하고 서로 바꾸기 bool 값 4개로 맞춰서 1234 각각의 버튼들에ㅐ 대한 상태 나타내기
        for (int i = 0; i + 1 < stackButtons.Length; i++)
        {
            if (stackButtons[i].activeSelf)
            {
                TitleManager.Instance.buyStackBuff[i] = true;
                stackButtons[i].SetActive(false);
                stackButtons[i + 1].SetActive(true);

                break;

            }
        }
    }
    public void BuySpeedBuff(int cost)
    {
        if (!TitleManager.Instance.BuySpeedBuff(cost))
        {
            return;
        }
        CoinRestore();
        //버프구매하면 구매할때 bool값 설정해서 트루일경우 lock은 셋액티브를 펄스로 유지 
        lockSpeedButton.SetActive(!TitleManager.Instance.buySpeedBuff);
        unLockSpeedButton.SetActive(TitleManager.Instance.buySpeedBuff);
        //완
    }
    public void BuyDamageBuff(int cost)
    {
        if (!TitleManager.Instance.BuyDamageBuff(cost))
        {
            return;
        }
        CoinRestore();

        lockDamageButton.SetActive(!TitleManager.Instance.buyDamageBuff);
        unLockDamageButton.SetActive(TitleManager.Instance.buyDamageBuff);
        //완
    }
    public void MinusCoin(int cost)
    {
        if (!TitleManager.Instance.MinusCoin(cost))
        {
            return;
        }

        CoinRestore();
        
    }

    private void UseCoinAchievement()
    {
        for (int i = 0; i < usedCoinCutline.Length; i++)
        {
            if (TitleManager.Instance.usedCoinCount > usedCoinCutline[i]&& !TitleManager.Instance.coinUsed[i])
            {
                //상점버튼 1 트루 
                usedCoinAchieveMentBtt[i].interactable = true;
                TitleManager.Instance.coinUsed[i] = true;
                //여기에 해당버튼의 상태 체크용
                TitleManager.Instance.usedCoinClicked[i] = true;
            }
        }
        
    }

    public void IncreaseCoin(int amount)
    {
        TitleManager.Instance.IncreaseCoin(amount);
        CoinRestore();
    }
    public void PlusDamageBuff()
    {
        TitleManager.Instance.PlusDamageStack();

    }
    public void SelectAbility(int ability)
    {
        TitleManager.Instance.selectedAbility = ability;
        //해당버튼의 속성 true 로 기억해두기
    }
    public void OpenStore()
    {
        storePanel.SetActive(true);
        RefreshStoreButton();
    }
    public void OpenAchieveMent()
    {
        achieveMentPanel.SetActive(true);
        RefreshAchieveMents();
        UseCoinAchievement();
    }
    //간단한 상태에 관한  오픈 뭐시기 이런것들은 여기로 옮기고 타이틀매니저는 싱글톤으로 구현하고 그러면 될지도? 
    public void StartGame()
    {
        BossMoveMent.bossClered = 0;
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void GetCompensation(int index)
    {
        //보상받기 누르면 트루false 반환 
        TitleManager.Instance.destroyBttClicked[index] = false;
    }
    public void GetWeaponCompen(int index)
    {
        TitleManager.Instance.wpdevelopClicked[index] = false;
    }
    public void GetUsedCoinCompens(int index)
    {
        TitleManager.Instance.usedCoinClicked[index] = false;
    }
    private void KeepButtonState()
    {
        //한번 활성화가 된 버튼은 누를때까지 유지한다.
        for (int i = 0; i < destroyAchieveMentsBtt.Length; i++)
        {
            destroyAchieveMentsBtt[i].interactable = TitleManager.Instance.destroyBttClicked[i];
        }

        for (int i = 0; i < 2; i++)
        {
            weapondevelopmentBtt[i].interactable = TitleManager.Instance.wpdevelopClicked[i];
        }

        for (int i = 0;i < 2; i++)
        {
            usedCoinAchieveMentBtt[i].interactable = TitleManager.Instance.usedCoinClicked[i];
        }
    }
}
