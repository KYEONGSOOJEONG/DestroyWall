using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WpSpawner : MonoBehaviour
{
    public static WpSpawner instance;

    public int[] upgradeCounts;
    private float startDelay = 1f;
    [HideInInspector]
    public float repeatRate = 0.4f;
    private int countIndex = 0;
    private int currentPrefabIndex = 0;
    private AudioSource spawnSound;

    public int CurrentPrefabIndex { get { return currentPrefabIndex; } }
    // Start is called before the first frame update

    void Start()
    {

        InvokeRepeating("SpawnWeapon", startDelay, repeatRate);
    }

    private void Awake()
    {
        instance = this;
        spawnSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        //이 걸 함수로 만들어두고 게임매니져에서 게임오버가 됐을 때 관리 해도됨


    }

    public void SpawnWeapon()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameObject weaponPrefab = PoolManager.instance.GetWeapon(currentPrefabIndex);

            if (weaponPrefab != null)
            {
                weaponPrefab.transform.position = transform.position;

                int destroyCount = Wall.destroyCount;

                if (destroyCount >= upgradeCounts[countIndex])
                {
                    countIndex++;
                    if (countIndex >= upgradeCounts.Length)
                    {
                        countIndex = upgradeCounts.Length - 1;
                    }
                    else
                    {
                        currentPrefabIndex++;
                        if (currentPrefabIndex >= PoolManager.instance.weaponPrefabs.Length)
                        {
                            currentPrefabIndex = PoolManager.instance.weaponPrefabs.Length - 1;
                        }
                    }
                }
            }
            //소환할때 소리내기
            spawnSound.Play();
        }

    }


    public void SpeedBuffStart()
    {
        CancelInvoke("SpawnWeapon");
        float skillRepeat = 0.2f;
        InvokeRepeating("SpawnWeapon",0f,skillRepeat);
    }
    public void SpeedBuffEnd()
    {
        CancelInvoke("SpawnWeapon");
        InvokeRepeating("SpawnWeapon", 0f, repeatRate);
    }
}
