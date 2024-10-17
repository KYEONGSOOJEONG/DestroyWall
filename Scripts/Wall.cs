using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public int hp;
    public float moveSpeed;
    public static int destroyCount = 0;
    public float xBoundary;

    private int initialHp;
    void Awake()
    {
        initialHp = hp;

    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

        if (transform.position.x < xBoundary)
        {
            
            PoolManager.instance.ReturnWall(gameObject, WallSpawner.Instance.CurrentPrefabIndex);
            //이 커런트 인덱스가 제대로 될려면 update에서 계속 확인해야하나?

            
        }

    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            destroyCount++;
            PoolManager.instance.ReturnWall(gameObject, WallSpawner.Instance.CurrentPrefabIndex);
            
        }
    }
    private void OnEnable()
    {
        ResetState();
    }

    private void ResetState()
    {
        hp = initialHp;
    }

    private void Start()
    {
        
    }
}
