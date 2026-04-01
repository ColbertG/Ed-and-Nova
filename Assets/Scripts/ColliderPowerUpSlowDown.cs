using UnityEngine;

public class ColliderPowerUpSlowDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControllerPlayer>() != null)
        {
            if (!ControllerPlayer.SlowDownActive)
            {
                ControllerSound.Instance.SlowDownPowerUps();
                ControllerPlayer.SlowDownActive = true;
                float speedLast = collision.gameObject.GetComponent<ControllerPlayer>().SetSpeed(0);
                float speedNow = speedLast - 1;
                ControllerPlayer.SpeedBackUp = speedNow + 1;
                collision.gameObject.GetComponent<ControllerPlayer>().SetSpeed(-(int)speedNow);
            }
            Destroy(gameObject);
        }
    }
}
