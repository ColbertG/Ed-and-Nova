using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    List<GameObject> PlayerShips;
    List<GameObject> PlayerCount = new List<GameObject>();
    bool BottomSpawnSet = false;
    bool LeftSpawnSet = false;
    bool RightSpawnSet = false;
    bool ActiveSpawnPlayer = false;
    int LevelSpawn = 0;
    GameObject clone = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public GameObject ActivePlayer() 
    {
        return clone;
    }
    public void SpawnRemover()
    {
        for (int i = 0; i < PlayerCount.Count; i++)
        {
            Destroy(PlayerCount[i]);
        }
    }
    public bool BarrierDone()
    {
        bool done = true;
        for (int i = 0; i < PlayerCount.Count; i++)
        {
            if (PlayerCount[i] != null)
            {
                done = false;
                break;
            }
        }
        if (done) PlayerCount = new List<GameObject>();
        return done;
    }
    public void SpawnLevel(int level, int spot)
    {
        LevelSpawn = level;
        ActiveSpawnPlayer = true;
        if (LevelSpawn < 0) LevelSpawn = 0;
        if (LevelSpawn > PlayerShips.Count) LevelSpawn = PlayerShips.Count - 1;
        if (spot == 1) BottomSpawnSet = true;
        if (spot == 2) LeftSpawnSet = true;
        if (spot == 3) RightSpawnSet = true;
        if (ActiveSpawnPlayer)
        {
            ActiveSpawnPlayer = false;
            clone = null;
            if (BottomSpawnSet)
            {
                BottomSpawnSet = false;
                Vector3 BottomSpawn = new Vector3(0, -4, 0);
                clone = Instantiate(PlayerShips[LevelSpawn], BottomSpawn, Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            if (LeftSpawnSet)
            {
                LeftSpawnSet = false;
                Vector3 LeftSpawn = new Vector3(0, 0, 0);
                clone = Instantiate(PlayerShips[LevelSpawn], LeftSpawn, Quaternion.Euler(0, 0, 90f)) as GameObject;
            }
            if (RightSpawnSet)
            {
                RightSpawnSet = false;
                Vector3 RightSpawn = new Vector3(0, 0, 0);
                clone = Instantiate(PlayerShips[LevelSpawn], RightSpawn, Quaternion.Euler(0, 0, -90f)) as GameObject;
            }
            PlayerCount.Add(clone);
        }
    }
}
