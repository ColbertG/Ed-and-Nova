using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPowerUpBarrier : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerBarrier;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
        {
            GameObject clone = Instantiate(PlayerBarrier, transform.position, transform.rotation) as GameObject;
            if (clone.GetComponent<ControllerPlayerBarrier>() != null)
                clone.GetComponent<ControllerPlayerBarrier>().MovingTarget(collision.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
