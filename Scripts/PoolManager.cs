using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public GameObject[] weaponPrefabs;
    private int weaponPoolSize = 8;
    public GameObject[] wallPrefabs;
    private int wallPoolSize = 8;
    public GameObject[] bossBulletPrefabs;
    private int bossBulletPoolSize = 8;

    private List<GameObject>[] weaponPools;
    private List<GameObject>[] wallPools;
    private List<GameObject>[] bossBulletPools;

    private AudioSource destroySound;
    private void Awake()
    {
        instance = this;
        destroySound = GetComponent<AudioSource>();

        weaponPools = new List<GameObject>[weaponPrefabs.Length];
        wallPools = new List<GameObject>[wallPrefabs.Length];
        bossBulletPools = new List<GameObject>[bossBulletPrefabs.Length];

        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            weaponPools[i] = new List<GameObject>();
            InitializedPool(weaponPools[i], weaponPrefabs[i], weaponPoolSize);
        }


        for (int i = 0; i < wallPrefabs.Length; i++)
        {
            wallPools[i] = new List<GameObject>();
            InitializedPool(wallPools[i], wallPrefabs[i], wallPoolSize);
        }

        for(int i = 0;i < bossBulletPrefabs.Length; i++)
        {
            bossBulletPools[i] = new List<GameObject>();
            InitializedPool(bossBulletPools[i], bossBulletPrefabs[i], bossBulletPoolSize);
        }
    }

    private void InitializedPool(List<GameObject> pool, GameObject prefab, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    private GameObject GetObjectFromPool(List<GameObject> pool, GameObject prefab)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newobj = Instantiate(prefab);
        pool.Add(newobj);
        return newobj;
    }

    public GameObject GetWeapon(int index)
    {
        GameObject weapon = GetObjectFromPool(weaponPools[index], weaponPrefabs[index]);
        return weapon;
    }

    public GameObject GetWall(int index)
    {
        GameObject wall = GetObjectFromPool(wallPools[index], wallPrefabs[index]);
        return wall;
    }

    public GameObject GetBossBullet(int index)
    {
        GameObject boss = GetObjectFromPool(bossBulletPools[index], bossBulletPrefabs[index]);
        return boss;
    }

    public void ReturnWeapon(GameObject weapon, int index)
    {
        ReturnObjectToPool(weaponPools[index], weapon);
    }

    public void ReturnWall(GameObject wall, int index)
    {
        ReturnObjectToPool(wallPools[index], wall);
    }

    private void ReturnObjectToPool(List<GameObject> pool, GameObject obj)
    {
        obj.SetActive(false);
    }

    void Update()
    {

    }

    
}
