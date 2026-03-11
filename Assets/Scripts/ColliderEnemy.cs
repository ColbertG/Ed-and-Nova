using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

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
    List<GameObject> RandomDrops;
    bool Exploed = false;
    Transform Target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderPlayerCpu>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderPlayerCpu>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ControllerPlayerBarrier>() != null)
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderLaser>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderLaser>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderPlayer>().DestructionPoints();
            if (HP <= 0)
            {
                Exploed = true;
                PlayerPrefs.SetInt("playerKills", PlayerPrefs.GetInt("playerKills", 0) + 1);
                PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + ScorePoints());
            }
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
                if (HP <= 0) 
                {
                    Exploed = true;
                    PlayerPrefs.SetInt("playerKills", PlayerPrefs.GetInt("playerKills", 0) + 1);
                    PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + ScorePoints());
                }
            }
        }
        if (Exploed)
        {
            GameObject clone2 = null;
            int randomSpawn = Random.Range(0, 20);
            bool spawn = randomSpawn == 0 || randomSpawn == 5 || randomSpawn == 10 || randomSpawn == 15;
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            if (spawn) 
            {
                int pick = Random.Range(0, RandomDrops.Count);
                clone2 = Instantiate(RandomDrops[pick], transform.position, transform.rotation) as GameObject;
                if(clone2.GetComponent<ControllerCrystal>() != null)
                    clone2.GetComponent<ControllerCrystal>().CrystalTarget(Target);
            }
            Exploed = false;
            ControllerSound.Instance.ShipExplosion();
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
    public void CrystalTarget(Transform target)
    {
        Target = target;
    }
}
