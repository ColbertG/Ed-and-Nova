using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCrystal : MonoBehaviour
{
    [SerializeField]
    int Rewards = 100;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            collision.gameObject.GetComponent<ColliderPlayer>().RewardPoints(Rewards);
            Destroy(gameObject);
        }
    }
}
