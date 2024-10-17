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
        //데미지를 주는 주최가 무기인데 무기에 takedamage를 써야하는지 의문 
        // 보스 본체에서 takedamage를 쓰는게 더 부드러운지 고민해볼것
        else if(other.CompareTag("Boss"))
        {
            BossMoveMent boss = other.GetComponent<BossMoveMent>();
            PoolManager.instance.ReturnWeapon(gameObject, WpSpawner.instance.CurrentPrefabIndex);

            boss.BossTakeDamage(damage);
        }
    }

    
    //skill버튼인 눌렸는지에 대한 검사를 받아서 계수 설정
    private int SkillValue()
    {
        //스킬 활성화시 공격력 2배
        return GameManager.instance.skillOn ? 2 : 1;
    }


}
