using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallSpawner : MonoBehaviour
{
    public static WallSpawner Instance;

    public Transform[] spawnPoints;
    [Header("Slider")]
    public Slider bossSpawnTimer;

    public int[] upgradeCounts;
    private int wallSpawnCount = 0;
    private int currentPrefabIndex = 0;
    private int countIndex = 0;
    [SerializeField]
    private int bossPhaseCount;
    public GameObject boss;
    private float startDelay = 2.0f;
    private float repeatDelay = 1.8f;
    public int CurrentPrefabIndex { get { return currentPrefabIndex; } }
    void Awake()
    {
        Instance = this;
        spawnPoints = GetComponentsInChildren<Transform>();

    }
    void Start()
    {
        InvokeRepeating("SpawnWall", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        bossSpawnTimer.value = (float)wallSpawnCount / bossPhaseCount;
    }

    void SpawnWall()
    {
        GameObject wallPrefab = PoolManager.instance.GetWall(currentPrefabIndex);

        if (wallPrefab != null)
        {
            
            wallPrefab.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;

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
                    if (currentPrefabIndex >= PoolManager.instance.wallPrefabs.Length)
                    {
                        currentPrefabIndex = PoolManager.instance.wallPrefabs.Length - 1;
                    }
                }
            }
            //소환개수 측정
            wallSpawnCount++;
            if (wallSpawnCount >= bossPhaseCount)
            {
                //보스소환
                boss.SetActive(true);
                GameManager.instance.PlayBossMusic();
            }
        }
    }

    public void StopSpawnEnemy()
    {
        CancelInvoke("SpawnWall");
    }

    
    
}
