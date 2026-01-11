using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPowerUpDestroy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPlayer>() != null)
        {
            collision.gameObject.GetComponent<ControllerPlayer>().ActiveCpu();
            Destroy(gameObject);
        }
    }
}
