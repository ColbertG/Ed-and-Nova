using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRocket : MonoBehaviour
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
            if (gameObject.CompareTag("Enemy"))
                Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderLaser>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderLaser>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            if (gameObject.CompareTag("Enemy"))
                Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderBoss>() != null)
        {
            if (gameObject.CompareTag("Player"))
                Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
        {
            if (!gameObject.CompareTag(collision.gameObject.tag)) 
            {
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
                if (HP <= 0) Exploed = true;
            }
        }
        if (collision.gameObject.GetComponent<ColliderMeteor>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderMeteor>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (Exploed)
        {
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Exploed = false;
            Destroy(gameObject);
        }
    }
    public int DestructionPoints(int dp = 0) 
    {
        DP = DP + dp;
        return DP;
    }
    public int HealthPoints(int hp = 0) 
    {
        HP = HP + hp;
        return HP;
    }
}
