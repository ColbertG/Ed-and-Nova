using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ControllerEnemy : MonoBehaviour
{
    [SerializeField]
    Transform MoveTarget;
    [SerializeField]
    float Speed = 1;
    [SerializeField]
    List<Rockets> Rocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OffScreen();
        RocketShot();
    }
    void OffScreen() 
    {
        float width = Screen.width;
        float height = Screen.height;
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(width, height, transform.position.z - Camera.main.transform.position.z));
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height / height, transform.position.z - Camera.main.transform.position.z));
        if (MoveTarget != null) transform.position = Vector3.MoveTowards(transform.position, MoveTarget.position, Speed * Time.deltaTime);
        if ((transform.position.x + 1) < pos2.x || (transform.position.x - 1) > pos.x) Destroy(gameObject);
        if ((transform.position.y + 1) < pos2.y || (transform.position.y - 1) > pos.y) Destroy(gameObject);
    }
    void RocketShot() 
    {
        if (Rocket.Count >= 1 && Time.time >= Rocket[0].NextFireTime)
        {
            GameObject clone = Instantiate(Rocket[0].MainRocket, Rocket[0].SpawnPoint.position, Rocket[0].SpawnPoint.rotation) as GameObject;
            Rocket[0].NextFireTime = Time.time + Rocket[0].FireRate;
        }
        if (Rocket.Count >= 2 && Time.time >= Rocket[1].NextFireTime)
        {
            GameObject clone = Instantiate(Rocket[1].MainRocket, Rocket[1].SpawnPoint.position, Rocket[1].SpawnPoint.rotation) as GameObject;
            Rocket[1].NextFireTime = Time.time + Rocket[1].FireRate;
        }
    }
}
