using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPowerUpSlowDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPlayer>() != null)
        {
            if (!ControllerPlayer.SlowDownActive) 
            {
                ControllerPlayer.SlowDownActive = true;
                float speedLast = collision.gameObject.GetComponent<ControllerPlayer>().SetSpeed(0);
                float speedNow = speedLast - 1;
                ControllerPlayer.SpeedBackUp = speedNow;
                collision.gameObject.GetComponent<ControllerPlayer>().SetSpeed(-(int)speedNow);
            }
            Destroy(gameObject);
        }
    }
}
