using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Bombs;
    [SerializeField]
    float FireRate = 0.5f;
    float NextFireTime = 0f;
    Vector3[] Spots = new Vector3[5];
    bool TopSpawnSet = false;
    bool LeftSpawnSet = false;
    bool RightSpawnSet = false;
    List<GameObject> BombCount = new List<GameObject>();
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
            int pickMeteor = Random.Range(LevelSpawnMin, LevelSpawn);
            if (TopSpawnSet)
            {
                Vector3 TopSpawn = new Vector3(Random.Range(Spots[0].x, Spots[1].x), Random.Range(Spots[0].y, Spots[1].y), 0);
                clone = Instantiate(Bombs[pickMeteor], TopSpawn, Quaternion.Euler(0, 0, 180f)) as GameObject;
            }
            if (LeftSpawnSet)
            {
                Vector3 LeftSpawn = new Vector3(Spots[0].x, Random.Range(Spots[2].y, Spots[1].y), 0);
                clone = Instantiate(Bombs[pickMeteor], LeftSpawn, Quaternion.Euler(0, 0, -90f)) as GameObject;
            }
            if (RightSpawnSet)
            {
                Vector3 RightSpawn = new Vector3(Spots[3].x, Random.Range(Spots[3].y, Spots[1].y), 0);
                clone = Instantiate(Bombs[pickMeteor], RightSpawn, Quaternion.Euler(0, 0, 90f)) as GameObject;
            }
            NextFireTime = Time.time + FireRate;
            BombCount.Add(clone);
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
    public void SpawnRemover()
    {
        for (int i = 0; i < BombCount.Count; i++)
        {
            Destroy(BombCount[i]);
        }
    }
    public int SpawnCounter(bool reset = false)
    {
        if (reset)
        {
            SpawnCount = 0;
        }
        return SpawnCount;
    }
    public bool BombDone()
    {
        bool done = true;
        for (int i = 0; i < BombCount.Count; i++)
        {
            if (BombCount[i] != null)
            {
                done = false;
                break;
            }
        }
        if (done) BombCount = new List<GameObject>();
        return done;
    }
    public void SpawnLevel(int level, int minLevel = 0)
    {
        LevelSpawn = level;
        LevelSpawnMin = minLevel;
        if(LevelSpawnMin >= LevelSpawn) LevelSpawnMin = LevelSpawn--;
        if(LevelSpawnMin < 0) LevelSpawnMin = 0;
        if (LevelSpawn > Bombs.Count) LevelSpawn = Bombs.Count;
        if (LevelSpawn <= 0) LevelSpawn = 1;
    }
    public float FaceingBomb()
    {
        if (TopSpawnSet) { return 0.0f; }
        if (LeftSpawnSet) { return 90.0f; }
        if (RightSpawnSet) { return -90.0f; }
        return 0.0f;
    }
    public void FaceingBomb(int spot)
    {
        if (spot == 0 || spot == 1)
        {
            RightSpawnSet = true;
            LeftSpawnSet = false;
            TopSpawnSet = false;
        }
        if (spot == 2)
        {
            TopSpawnSet = true;
            RightSpawnSet = false;
            LeftSpawnSet = false;
        }
        if (spot == 3 || spot == 4)
        {
            LeftSpawnSet = true;
            TopSpawnSet = false;
            RightSpawnSet = false;
        }
    }
}
