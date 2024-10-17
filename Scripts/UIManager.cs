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
        //buff,skill �ʱ�ȭ
        TitleManager.Instance.ResetData();
        CoinRestore();
        //coin ���� 
        RefreshAchieveMents();
        RefreshStoreButton();
        // ���� �ʱ�ȭ
        WeapoonAchieveCheck();
        //Ÿ��Ʋ�Ŵ����� �ִ� ���°��� true�̸� �ش��ư���� true�����Ѵٱٵ� ������ �ٽ� Ʈ�簡 �Ǵµ�
        //�� ������ ��ư��������
        KeepButtonState();
    }

    private void Awake()
    {
        //�̷��� ����Ǹ� �Ź� ������Ʈ�� �����ɴ� bool ������ ���� �����ɰŰ� �׷��� false�� ��ȯ�ҵ�
        //���� ��ũ��Ʈ������ ������Ʈ�� �¾�Ƽ�� Ȱ��ȭ�� �����ְ� �����ü�� Title����
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
        //��
    }

    private void RefreshAchieveMents()
    {
        //Ÿ��Ʋ�Ŵ������� �޾ƿ� bool ���� ���� ���⿡ �Ҵ��ϴ� ������Ʈ ���� interactable���� �����ȴ�. Ÿ��Ʋ�Ŵ��������� bool�� �� ����
        int count = TitleManager.Instance.destroyCountForSend;

        for (int i = 0; i < destroyAchieveMentsBtt.Length; i++)
        {
            //�޾ƿ� bool���� ���� ��ư Ȱ��ȭ ���� 
            if (count >= destroyCutline[i] && !TitleManager.Instance.clearedAchievements[i])
            {
                destroyAchieveMentsBtt[i].interactable = true;
                TitleManager.Instance.clearedAchievements[i] = true;
                TitleManager.Instance.destroyBttClicked[i] = true;
            }
        }
        //��
    }
    private void CoinRestore()
    {
        coinText.text = TitleManager.Instance.coin.ToString();
        //��
    }

    private void RefreshStoreButton()
    {
        // ���� Ÿ��Ʋ�Ŵ����� �ִ� �Ұ��� Ʈ���� �� �ش� ��ư�� �¾�Ƽ�� Ʈ�翩���Ѵ�. �̹�ư�� ������ ������ �ϸ�ȴ� 
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

        //3�� �Ҵ��ϰ� ���� �ٲٱ� bool �� 4���� ���缭 1234 ������ ��ư�鿡�� ���� ���� ��Ÿ����
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
        //���������ϸ� �����Ҷ� bool�� �����ؼ� Ʈ���ϰ�� lock�� �¾�Ƽ�긦 �޽��� ���� 
        lockSpeedButton.SetActive(!TitleManager.Instance.buySpeedBuff);
        unLockSpeedButton.SetActive(TitleManager.Instance.buySpeedBuff);
        //��
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
        //��
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
                //������ư 1 Ʈ�� 
                usedCoinAchieveMentBtt[i].interactable = true;
                TitleManager.Instance.coinUsed[i] = true;
                //���⿡ �ش��ư�� ���� üũ��
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
        //�ش��ư�� �Ӽ� true �� ����صα�
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
    //������ ���¿� ����  ���� ���ñ� �̷��͵��� ����� �ű�� Ÿ��Ʋ�Ŵ����� �̱������� �����ϰ� �׷��� ������? 
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
        Application.Quit(); // ���ø����̼� ����
#endif
    }

    public void GetCompensation(int index)
    {
        //����ޱ� ������ Ʈ��false ��ȯ 
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
        //�ѹ� Ȱ��ȭ�� �� ��ư�� ���������� �����Ѵ�.
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
