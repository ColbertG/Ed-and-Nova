using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBoss : MonoBehaviour
{
    [SerializeField]
    int HP = 100;
    [SerializeField]
    int DP = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
            if (collision.gameObject.CompareTag("Player"))
            HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
        if (HP <= 0) 
        {
            Destroy(gameObject);
        }
    }
    public int HealthPoints(int changeHealth = 0)
    {
        HP = HP + changeHealth;
        return HP;
    }
    public int DestructionPoints()
    {
        return DP;
    }
}
