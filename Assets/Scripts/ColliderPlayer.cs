using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    [SerializeField]
    int HP = 100; 
    [SerializeField]
    int DP = 1;
    [SerializeField]
    int Rewards = 10;
    [SerializeField]
    GameObject Explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderLaser>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderLaser>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
            if (collision.gameObject.CompareTag("Enemy"))
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderMeteor>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderMeteor>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderBomb>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderBomb>().DestructionPoints();
        if (HP <= 0) 
        {
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
    public int HealthPoints(int changeHealth = 0)
    {
        HP = HP + changeHealth;
        return HP;
    }
    public int DestructionPoints(int dp = 0)
    {
        DP = DP + dp;
        return DP;
    }
    public int RewardPoints(int reward = 0) 
    {
        Rewards += reward;
        return Rewards;    
    } 
}
