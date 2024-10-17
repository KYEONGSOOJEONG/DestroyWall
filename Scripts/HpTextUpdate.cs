using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpTextUpdate : MonoBehaviour
{
    public GameObject boss;

    [SerializeField] private Vector3 offset;

    private int bossHp;
    private TextMeshProUGUI bossHpText;
    
    void Start()
    {

    }

    private void Awake()
    {
        bossHpText = gameObject.GetComponent<TextMeshProUGUI>();
        //얘는 겟컴포넌트로 텍스트 가져와야하고 보스에 할당된것의 겟컴포넌트 보스무브먼트 가져오고 거기서hp가져온다
        bossHp = boss.GetComponent<BossMoveMent>().BossHpForText;
    }
    // Update is called once per frame
    void Update()
    {
        if(boss.activeInHierarchy)
        {
            bossHp = boss.GetComponent<BossMoveMent>().BossHpForText;
            bossHpText.text = bossHp.ToString();

            transform.position = Camera.main.WorldToScreenPoint(boss.transform.position + offset);
        }
        
    }
}
