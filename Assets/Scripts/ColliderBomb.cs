using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBomb : MonoBehaviour
{
    [SerializeField]
    int DP = 1;
    [SerializeField]
    int HP = 1;
    [SerializeField]
    GameObject Explosion;
    bool Exploed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderBarrier>() != null)
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
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
        {
            if (collision.gameObject.CompareTag("Player")) 
            {
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
                if (HP <= 0) Exploed = true;
            }
                
        }
        if (Exploed)
        {
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Exploed = false;
            Destroy(gameObject);
        }
    }
    public int DestructionPoints()
    {
        return DP;
    }
}
