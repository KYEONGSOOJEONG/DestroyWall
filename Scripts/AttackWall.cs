using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWall : MonoBehaviour
{

    public float speed;
    public int damage;
    public float xBoundary;

    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movingright = Vector3.right * speed * GameManager.instance.speedBuffValue * Time.fixedDeltaTime;

        transform.position += movingright;

        if (transform.position.x > xBoundary)
        {
            PoolManager.instance.ReturnWeapon(gameObject, WpSpawner.instance.CurrentPrefabIndex);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Wall wall = other.GetComponent<Wall>();

            if (wall != null)
            {
                int finalDamage = damage * SkillValue() + TitleManager.Instance.damageStack;

                wall.TakeDamage(finalDamage);
                PoolManager.instance.ReturnWeapon(gameObject, WpSpawner.instance.CurrentPrefabIndex);
            }

        }
        //�������� �ִ� ���ְ� �����ε� ���⿡ takedamage�� ����ϴ��� �ǹ� 
        // ���� ��ü���� takedamage�� ���°� �� �ε巯���� ����غ���
        else if(other.CompareTag("Boss"))
        {
            BossMoveMent boss = other.GetComponent<BossMoveMent>();
            PoolManager.instance.ReturnWeapon(gameObject, WpSpawner.instance.CurrentPrefabIndex);

            boss.BossTakeDamage(damage);
        }
    }

    
    //skill��ư�� ���ȴ����� ���� �˻縦 �޾Ƽ� ��� ����
    private int SkillValue()
    {
        //��ų Ȱ��ȭ�� ���ݷ� 2��
        return GameManager.instance.skillOn ? 2 : 1;
    }


}
