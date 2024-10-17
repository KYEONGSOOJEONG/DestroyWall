using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;
    
    [Space(10f)]
    public int coin ;
    [HideInInspector]
    public int selectedAbility;
    public int damageStack;
    [HideInInspector]
    public bool[] clearedAchievements = new bool[4];
    [HideInInspector]
    public bool[] isdeveloped = new bool[2];
    [HideInInspector]
    public bool buySpeedBuff;
    [HideInInspector]
    public bool buyDamageBuff;
    [HideInInspector]
    public bool[] buyStackBuff = new bool[4];
    [HideInInspector]
    public int destroyCountForSend;
    [HideInInspector]
    public int wpCurrentIndex;
    [HideInInspector]
    public bool[] destroyBttClicked= new bool[4];
    [HideInInspector]
    public bool[] wpdevelopClicked = new bool[2];
    [HideInInspector]
    public bool[] usedCoinClicked = new bool[2];
    [HideInInspector]
    public int usedCoinCount=0;
    [HideInInspector]
    public bool[] coinUsed = new bool[2];
    void Start()
    {
        
        
        //ý ��ġ���Ʈ ����
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            clearedAchievements = new bool[4];
            destroyCountForSend = 0;
            coin = 0;
            damageStack = 0;
            wpCurrentIndex = 0;
            PlayerPrefs.SetInt("GetCoin", 0);
            PlayerPrefs.Save();
            DontDestroyOnLoad(gameObject);
        }
        
    }

    
    
    

    public void ResetData()
    {
        selectedAbility = 0;

        if (PlayerPrefs.HasKey("GetCoin"))
        {
            coin = PlayerPrefs.GetInt("GetCoin");
        }
    }
    
    public void IncreaseCoin(int amount)
    {
        coin += amount;
    }

    public bool MinusCoin(int price)
    {
        //���� ������ ���ݺ��� ���ٸ� ���Ű���
        if (coin < price)
        {
            return false;
        }
        coin -= price;
        usedCoinCount += price;
        //���⿡ �Լ��� �߰��ؾ��ϳ�?
        return true;
    }

    

    public void PlusDamageStack()
    {
        damageStack++;
    }

    //���ӿ��������� �ѹ��� �����ϸ� �Ǵ� �Լ��� �����Ű�� �ɵ�
    

    public bool BuyDamageBuff(int cost)
    {
        //��� �̶� �� �ΰ� ���� �ϳ��� �� ��Ƽ�� Ʈ�� �޽� �ݴ�� �ٲ�� ���̳ʽ� ���� 20
        //�׷��� ���� �Լ��� �ֵ帮���ʷ� �߰��ϱ� �߰��Ҷ� ������ȭ�� �������� ���� 
        if (coin < cost)
        {
            return false;
        }
        MinusCoin(cost);
        buyDamageBuff = true; 
        return true;
    }

    public bool BuySpeedBuff(int cost)
    {
        if (coin < cost)
        {
            return false;
        }
        MinusCoin(cost);
        buySpeedBuff = true;
        return true;
    }

    public bool BuyStackBuff(int cost)
    {
        //�������� ������ ����ϸ� ���� �ƴϸ� ����
        //����Ҷ� ������ ���� �¾�Ƽ�� �޽� ������ Ʈ��� ���̳ʽ� �����ϰ�, ��������� ����
        if (coin < cost)
        {
            return false;
        }
        MinusCoin(cost);
        PlusDamageStack();

        return true;
    }

    
}
