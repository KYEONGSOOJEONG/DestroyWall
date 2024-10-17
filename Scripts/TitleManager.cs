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
        
        
        //첵 어치브먼트 실행
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
        //가진 코인이 가격보다 많다면 구매가능
        if (coin < price)
        {
            return false;
        }
        coin -= price;
        usedCoinCount += price;
        //여기에 함수를 추가해야하나?
        return true;
    }

    

    public void PlusDamageStack()
    {
        damageStack++;
    }

    //게임오버했을때 한번만 실행하면 되는 함수로 적용시키면 될듯
    

    public bool BuyDamageBuff(int cost)
    {
        //언락 이랑 락 두개 들어간거 하나는 셋 액티브 트루 펄스 반대로 바뀌고 마이너스 코인 20
        //그렇게 만든 함수를 애드리스너로 추가하기 추가할때 보유재화가 많을때만 실행 
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
        //눌렀을때 코인이 충분하면 실행 아니면 리턴
        //충분할땐 누르면 본인 셋액티브 펄스 다음꺼 트루로 마이너스 코인하고, 대미지스택 실행
        if (coin < cost)
        {
            return false;
        }
        MinusCoin(cost);
        PlusDamageStack();

        return true;
    }

    
}
