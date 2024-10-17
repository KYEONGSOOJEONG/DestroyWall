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
        //��� ��������Ʈ�� �ؽ�Ʈ �����;��ϰ� ������ �Ҵ�Ȱ��� ��������Ʈ ���������Ʈ �������� �ű⼭hp�����´�
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
