using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ColliderEnemy : MonoBehaviour
{
    [SerializeField]
    int Score = 1;
    [SerializeField]
    int DP = 1;
    [SerializeField]
    int HP = 1;
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    List<GameObject> PowerUps;
    bool Exploed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderLaser>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderLaser>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderPlayer>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderBarrier>() != null)
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderMeteor>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderMeteor>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
        {
            if (collision.gameObject.CompareTag("Player")) 
            {
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();

                PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + ScorePoints());
                if (HP <= 0) Exploed = true;
            }
        }
        if (Exploed)
        {
            GameObject clone2 = null;
            int randomSpawn = Random.Range(0, 10);
            bool spawn = randomSpawn == 0 || randomSpawn == 3 || randomSpawn == 6 || randomSpawn == 9;
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            if (spawn) 
            {
                int pick = Random.Range(0, PowerUps.Count);
                clone2 = Instantiate(PowerUps[pick], transform.position, transform.rotation) as GameObject;
            }
            Exploed = false;
            Destroy(gameObject);
        }
    }
    public int DestructionPoints()
    {
        return DP;
    }
    public int ScorePoints()
    {
        return Score;
    }
}
