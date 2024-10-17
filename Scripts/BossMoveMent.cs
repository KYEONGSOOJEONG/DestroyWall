using System.Collections;
using UnityEngine;

public class BossMoveMent : MonoBehaviour
{

    [SerializeField] private int bossHp;
    public int BossHpForText { get { return bossHp; } }
    [SerializeField] private float bossSpeed;
    [Header("Transform")]
    public GameObject spawnPoint;
    public Transform playerPoint;
    [Header("UI")]
    public ParticleSystem deadParticle;
    public static int bossClered = 0;
    public GameObject hpText;
    public GameObject[] bossTimer;

    private SpriteRenderer spriteRenderer;
    private Vector3 targetPosition;
    private float spawnSpeed = 3f;
    private float moveAttackSpeed = 2f;
    private float maxBoundaryY = 0.4f;
    private float minBoundaryY = -1.13f;
    private bool isAttacking;
    private int totalHp;
    void Start()
    {

        MoveInView();

    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalHp = bossHp;
    }
    // Update is called once per frame
    void Update()
    {

        //����

        //�������� �Ѿ� �߻��ϴ� ���� ���� �ϱ� for������ �ϸ�ɵ� 
    }

    void ThrowRock()
    {
        //���������� �߻�
        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = PoolManager.instance.GetBossBullet(0);
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = playerPoint.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.2f, 0f), Random.Range(-0.2f, 0.4f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

    }
    private void OnEnable()
    {
        //��� �ٲ��? hp�� ��Ÿ���� ���ڸ� ǥ���Ѵ� ���̴°� ������Ʈ���� �������Ѵ�
        WallSpawner.Instance.StopSpawnEnemy();
        // ������ ��Ÿ���鼭 ����ȯ ����
        hpText.SetActive(true);
        bossTimer[0].SetActive(false);
        bossTimer[1].SetActive(false);
    }

    public void BossTakeDamage(int damage)
    {

        bossHp -= damage;

        StopCoroutine(HitColorChange());
        // ������� �ӵ��� ������ �׷��� �ʹ� ���� ȣ��Ǵµ�
        StartCoroutine(HitColorChange());
        //������ �������� �Դ´� �������� 3�� ������ ������������� �����Ѵ�. 

        if (bossHp / totalHp < 0.7f && !isAttacking)
        {
            StartCoroutine(Phase1Attack());
        }

        if (bossHp <= 0)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(hpText);
            bossClered++;
            GameManager.instance.GameOver();
            //Ŭ�����ϸ� ���� 
        }
    }

    void MoveInView()
    {
        //����
        Vector3 spawnSpot = spawnPoint.transform.position;
        StartCoroutine(MoveToPosition(spawnSpot));
        if (!GameManager.instance.isGameOver)
        {
            InvokeRepeating("ThrowRock", 1.8f, 1.4f);
        }
        
    }

    void AttackPlayer(Vector3 attackPos)
    {
        //vector2�ΰ��� 3�� ����ľ� ��������� ��
        transform.position = Vector2.MoveTowards(gameObject.transform.position, attackPos, moveAttackSpeed * Time.deltaTime);


    }





    IEnumerator MoveToPosition(Vector3 target)
    {

        while (Vector2.Distance(transform.position, target) > 0.05f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, spawnPoint.transform.position, spawnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Phase1Attack()
    {
        targetPosition = playerPoint.position;

        isAttacking = true;

        while (Vector2.Distance(transform.position, targetPosition) > 0.04f)
        {
            AttackPlayer(targetPosition);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(BackToSpawnPos());

    }

    private IEnumerator HitColorChange()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.08f);

        spriteRenderer.color = Color.white;

    }

    IEnumerator BackToSpawnPos()
    {
        float currentY = spawnPoint.transform.position.y + Random.Range(-0.4f, 0.4f);

        if (currentY >= maxBoundaryY)
        {
            currentY = maxBoundaryY;
        }
        else if (currentY <= minBoundaryY)
        {
            currentY = minBoundaryY;
        }
        Vector3 returnpos = new Vector3(spawnPoint.transform.position.x, currentY, 0);

        while (Vector2.Distance(transform.position, returnpos) > 0.09f)
        {
            transform.position = Vector2.MoveTowards(gameObject.transform.position, returnpos, moveAttackSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(0.09f);

        isAttacking = false;



    }
}
