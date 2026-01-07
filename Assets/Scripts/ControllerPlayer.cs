using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ControllerPlayer : MonoBehaviour
{
    [SerializeField]
    float Speed = 1f;
    [SerializeField]
    Transform Target;
    [SerializeField]
    List<Rockets> Rocket;
    float Angle;
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
            if (touch.phase == TouchPhase.Moved) 
            {
                Vector2 pos = touch.position;
                Vector3 position = Camera.main.ScreenToWorldPoint( new Vector3(pos.x, pos.y, transform.position.z - Camera.main.transform.position.z) );

                //position the player
                if(transform.position != position)
                    transform.position = Vector3.Lerp(transform.position, position, Speed * Time.deltaTime);
            }
        }

        if (Target != null)
        {
            Vector2 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle * -1)), Speed * Time.deltaTime);
        }
        else  
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, Angle)), Speed * Time.deltaTime);
        }

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
            //if (Rocket.Count >= 1 && touch.phase == TouchPhase.Moved && Time.time >= Rocket[0].NextFireTime)
            //{
            //    GameObject clone = Instantiate(Rocket[0].MainRocket, Rocket[0].SpawnPoint.position, Rocket[0].SpawnPoint.rotation) as GameObject;
            //    Rocket[0].NextFireTime = Time.time + Rocket[0].FireRate;
            //    clone.GetComponent<ColliderRocket>().DestructionPoints(PlayerPrefs.GetInt("rocketDp", 0));
            //    clone.GetComponent<ColliderRocket>().HealthPoints(PlayerPrefs.GetInt("rocketHp", 0));
            //}
            //if (Rocket.Count >= 2 && touch.phase == TouchPhase.Moved && Time.time >= Rocket[1].NextFireTime)
            //{
            //    GameObject clone = Instantiate(Rocket[1].MainRocket, Rocket[1].SpawnPoint.position, Rocket[1].SpawnPoint.rotation) as GameObject;
            //    Rocket[1].NextFireTime = Time.time + Rocket[1].FireRate;
            //    clone.GetComponent<ColliderRocket>().DestructionPoints(PlayerPrefs.GetInt("rocketDp", 0));
            //    clone.GetComponent<ColliderRocket>().HealthPoints(PlayerPrefs.GetInt("rocketHp", 0));
            //}
        }

    }
    public void AngleControll(float angle) 
    {
        Angle = angle;
    }
    public void SetTarget(Transform Boss) 
    {
        Target = Boss;
    }
    public void SetSpeed(int speed) 
    {
        Speed = Speed + speed;
    }
}
