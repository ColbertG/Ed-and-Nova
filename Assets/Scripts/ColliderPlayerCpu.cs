using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayerCpu : MonoBehaviour
{
    [SerializeField]
    int DP = 1;
    [SerializeField]
    GameObject Explosion;
    bool Exploed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderLaser>() != null) 
            Exploed = true;

        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
            if (collision.gameObject.CompareTag("Enemy")) 
                Exploed = true;

        if (collision.gameObject.GetComponent<ColliderMeteor>() != null)
        {
            Exploed = true;
            PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + collision.gameObject.GetComponent<ColliderMeteor>().ScorePoints());
        }
        
        if (collision.gameObject.GetComponent<ColliderBomb>() != null)
        {
            Exploed = true;
            PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + collision.gameObject.GetComponent<ColliderBomb>().ScorePoints());
        }

        if (collision.gameObject.GetComponent<ColliderEnemy>() != null)
        {
            Exploed = true;
            PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + collision.gameObject.GetComponent<ColliderEnemy>().ScorePoints());
        }

        if (Exploed)
        {
            Exploed = false;
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            gameObject.SetActive(false);
        }
    }
    public int DestructionPoints(int dp = 0)
    {
        DP = DP + dp;
        return DP;
    }
}
