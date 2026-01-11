using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ControllerPlayerCpu : MonoBehaviour
{
    [SerializeField]
    List<Rockets> Rocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            for (int i = 0; i < Rocket.Count; i++)
            {
                if (touch.phase == TouchPhase.Moved && Time.time >= Rocket[i].NextFireTime)
                {
                    GameObject clone = Instantiate(Rocket[i].MainRocket, Rocket[i].SpawnPoint.position, Rocket[i].SpawnPoint.rotation) as GameObject;
                    Rocket[i].NextFireTime = Time.time + Rocket[i].FireRate;
                    clone.GetComponent<ColliderRocket>().DestructionPoints(PlayerPrefs.GetInt("rocketDp", 0));
                    clone.GetComponent<ColliderRocket>().HealthPoints(PlayerPrefs.GetInt("rocketHp", 0));
                }
            }
        }
    }
}
