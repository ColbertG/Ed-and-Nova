using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrier : MonoBehaviour
{
    [SerializeField]
    List<GameObject> Barrier;
    List<GameObject> BarrierCount = new List<GameObject>();
    bool BottomSpawnSet = false;
    bool LeftSpawnSet = false;
    bool RightSpawnSet = false;
    bool ActiveSpawnBarrier = false;
    int LevelSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveSpawnBarrier)
        {
            ActiveSpawnBarrier = false;
            GameObject clone = null;
            if (BottomSpawnSet)
            {
                BottomSpawnSet = false;
                Vector3 BottomSpawn = new Vector3(0, 0, 0);
                clone = Instantiate(Barrier[LevelSpawn], BottomSpawn, Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            if (LeftSpawnSet)
            {
                LeftSpawnSet = false;
                Vector3 LeftSpawn = new Vector3(0, 0, 0);
                clone = Instantiate(Barrier[LevelSpawn], LeftSpawn, Quaternion.Euler(0, 0, -90f)) as GameObject;
            }
            if (RightSpawnSet)
            {
                RightSpawnSet = false;
                Vector3 RightSpawn = new Vector3(0, 0, 0);
                clone = Instantiate(Barrier[LevelSpawn], RightSpawn, Quaternion.Euler(0, 0, 90f)) as GameObject;
            }
            BarrierCount.Add(clone);
        }
    }
    public void SpawnRemover()
    {
        for (int i = 0; i < BarrierCount.Count; i++)
        {
            Destroy(BarrierCount[i]);
        }
    }
    public bool BarrierDone()
    {
        bool done = true;
        for (int i = 0; i < BarrierCount.Count; i++)
        {
            if (BarrierCount[i] != null)
            {
                done = false;
                break;
            }
        }
        if (done) BarrierCount = new List<GameObject>();
        return done;
    }
    public void SpawnLevel(int level, int spot)
    {
        LevelSpawn = level;
        ActiveSpawnBarrier = true; 
        if (LevelSpawn < 0) LevelSpawn = 0;
        if (LevelSpawn > Barrier.Count) LevelSpawn = Barrier.Count - 1;
        if (spot == 1) BottomSpawnSet = true;
        if (spot == 2) LeftSpawnSet = true;
        if (spot == 3) RightSpawnSet = true;
    }
}
