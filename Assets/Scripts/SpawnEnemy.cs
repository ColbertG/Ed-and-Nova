using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Enemy;
    [SerializeField]
    float FireRate = 0.5f;
    float NextFireTime = 0f;
    Vector3[] Spots = new Vector3[5];
    bool TopSpawnSet = false;
    bool LeftSpawnSet = false;
    bool RightSpawnSet = false;
    int BossSpot;
    List<GameObject> EnemyCount = new List<GameObject>();
    int LevelSpawnMin = 0;
    int LevelSpawn = 1;
    int SpawnCount = 0;
    void Awake()
    {
        float width = Screen.width;
        float height = Screen.height;
        Spots[0] = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height, transform.position.z - Camera.main.transform.position.z));
        Spots[1] = Camera.main.ScreenToWorldPoint(new Vector3(width, height, transform.position.z - Camera.main.transform.position.z));

        Spots[2] = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height / height, transform.position.z - Camera.main.transform.position.z));

        Spots[3] = Camera.main.ScreenToWorldPoint(new Vector3(width, height / height, transform.position.z - Camera.main.transform.position.z));

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextFireTime)
        {
            GameObject clone = null;
            int pickEnemy = Random.Range(LevelSpawnMin, LevelSpawn);
            if (TopSpawnSet)
            {
                Vector3 TopSpawn = new Vector3(Random.Range(Spots[0].x, Spots[1].x), Random.Range(Spots[0].y, Spots[1].y), 0);
                if(BossSpot == 1)
                {
                    clone = Instantiate(Enemy[pickEnemy], TopSpawn, Quaternion.Euler(0, 0, 135f));
                }
                if (BossSpot == 2)
                {
                    clone = Instantiate(Enemy[pickEnemy], TopSpawn, Quaternion.Euler(0, 0, 180f));
                }
                if (BossSpot == 3)
                {
                    clone = Instantiate(Enemy[pickEnemy], TopSpawn, Quaternion.Euler(0, 0, -135f));
                }
            }
            if (LeftSpawnSet)
            {
                Vector3 LeftSpawn = new Vector3(Spots[0].x, Random.Range(Spots[2].y, Spots[1].y), 0);
                clone = Instantiate(Enemy[pickEnemy], LeftSpawn, Quaternion.Euler(0, 0, -90f)) as GameObject;
            }
            if (RightSpawnSet)
            {
                Vector3 RightSpawn = new Vector3(Spots[3].x, Random.Range(Spots[3].y, Spots[1].y), 0);
                clone = Instantiate(Enemy[pickEnemy], RightSpawn, Quaternion.Euler(0, 0, 90f)) as GameObject;
            }
            NextFireTime = Time.time + FireRate;
            EnemyCount.Add(clone);
            SpawnCount++;
        }
    }

    void LateUpdate()
    {

        float width = Screen.width;
        float height = Screen.height;
        Spots[0] = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height, transform.position.z - Camera.main.transform.position.z));
        Spots[1] = Camera.main.ScreenToWorldPoint(new Vector3(width, height, transform.position.z - Camera.main.transform.position.z));

        Spots[2] = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height / height, transform.position.z - Camera.main.transform.position.z));

        Spots[3] = Camera.main.ScreenToWorldPoint(new Vector3(width, height / height, transform.position.z - Camera.main.transform.position.z));
    }
    public void LookAtPlayer(Transform target) 
    {
        foreach (GameObject enemy in EnemyCount) 
        {
            if(enemy != null) enemy.GetComponent<ControllerEnemy>().SetTarget(target);
        }
    }
    public void SpawnRate(float sec) 
    {
        FireRate = sec;
    }
    public void SpawnRemover()
    {
        for (int i = 0; i < EnemyCount.Count; i++)
        {
            Destroy(EnemyCount[i]);
        }
    }
    public int SpawnCounter(bool reset = false) 
    {
        if (reset) SpawnCount = 0;
        return SpawnCount;
    }
    public bool EnemyDone()
    {
        bool done = true;
        for (int i = 0; i < EnemyCount.Count; i++)
        {
            if (EnemyCount[i] != null)
            {
                done = false;
                break;
            }
        }
        return done;
    }
    public void SpawnLevel(int level, int minLevel = 0)
    {
        LevelSpawn = level;
        LevelSpawnMin = minLevel;
        if (LevelSpawnMin >= LevelSpawn) LevelSpawnMin = LevelSpawn--;
        if (LevelSpawnMin < 0) LevelSpawnMin = 0;
        if (LevelSpawn > Enemy.Count) LevelSpawn = Enemy.Count;
        if (LevelSpawn <= 0) LevelSpawn = 1;
    }
    public float FaceingEnemy()
    {
        if (TopSpawnSet) 
        {
            if (BossSpot == 1) return -45;
            if (BossSpot == 3) return 45;
            return 0.0f;
        }
        if (LeftSpawnSet) { return 90.0f; }
        if (RightSpawnSet) { return -90.0f; }
        return 0.0f;
    }
    public void FaceingEnemy(int spot)
    {
        BossSpot = spot;
        if (spot == 0 )
        {
            RightSpawnSet = true;
            LeftSpawnSet = false;
            TopSpawnSet = false;
        }
        if (spot == 1 || spot == 2 || spot == 3)
        {
            TopSpawnSet = true;
            RightSpawnSet = false;
            LeftSpawnSet = false;
        }
        if (spot == 4)
        {
            LeftSpawnSet = true;
            TopSpawnSet = false;
            RightSpawnSet = false;
        }
    }
}
