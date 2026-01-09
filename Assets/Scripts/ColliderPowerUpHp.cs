using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPowerUpHp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            collision.gameObject.GetComponent<ColliderPlayer>().HealthPoints(3);
            Destroy(gameObject);
        }
    }
}
