using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPowerUpHp : MonoBehaviour
{
    [SerializeField]
    int Hp = 1;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            ControllerSound.Instance.HpPowerUps();
            collision.gameObject.GetComponent<ColliderPlayer>().HealthPoints(Hp);
            Destroy(gameObject);
        }
    }
}
