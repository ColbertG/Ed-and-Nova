using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class ControllerPlayer : MonoBehaviour
{
    [SerializeField]
    float Speed = 1f;
    [SerializeField]
    Transform Target;
    [SerializeField]
    List<Rockets> Rocket;
    [SerializeField]
    List<GameObject> ShipCpu;
    float Angle;
    bool Cpu1 = false; 
    bool Cpu2 = false;
    public static bool SlowDownActive = false;
    public static float SpeedBackUp = 0;
    // Start is called before the first frame update
    void Start()
    {
        ShipCpu[0].SetActive(false);
        ShipCpu[1].SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!ShipCpu[0].activeSelf) Cpu1 = false;
        if (!ShipCpu[1].activeSelf) Cpu2 = false;

        PlayerOffScreen();
        PlayerMove();
        PlayerLook();
        PlayerShot();

        if (SlowDownActive && SpeedBackUp != 0) 
        {
            StartCoroutine(SlowDownNow());
        }
    }
    IEnumerator SlowDownNow()
    {
        yield return new WaitForSeconds(5.0f);
        SetSpeed((int)SpeedBackUp);
        SlowDownActive = false;
        SpeedBackUp = 0;
    }
    public void ActiveCpu() 
    {
        if (!Cpu1)
        {
            Cpu1 = true;
            ShipCpu[0].SetActive(true);
            ShipCpu[0].GetComponent<ColliderPlayerCpu>().DestructionPoints(PlayerPrefs.GetInt("playerDp", 0));
        }
        else if (!Cpu2) 
        {
            Cpu2 = true;
            ShipCpu[1].SetActive(true);
            ShipCpu[1].GetComponent<ColliderPlayerCpu>().DestructionPoints(PlayerPrefs.GetInt("playerDp", 0));
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
    public float SetSpeed(int speed) 
    {
        Speed = Speed + speed;
        return Speed;
    }
    void PlayerOffScreen()
    {
        float width = Screen.width;
        float height = Screen.height;
        UnityEngine.Vector3 pos = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width, height, transform.position.z - Camera.main.transform.position.z));
        UnityEngine.Vector3 pos2 = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width / width, height / height, transform.position.z - Camera.main.transform.position.z));
        if (transform.position.x > pos.x) transform.position = UnityEngine.Vector3.Lerp(transform.position, new UnityEngine.Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Speed * Time.deltaTime);
        if (transform.position.y > pos.y) transform.position = UnityEngine.Vector3.Lerp(transform.position, new UnityEngine.Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Speed * Time.deltaTime);
        if (transform.position.x < pos2.x) transform.position = UnityEngine.Vector3.Lerp(transform.position, new UnityEngine.Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Speed * Time.deltaTime);
        if (transform.position.y < pos2.y) transform.position = UnityEngine.Vector3.Lerp(transform.position, new UnityEngine.Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Speed * Time.deltaTime);
    }
    void PlayerMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                UnityEngine.Vector2 pos = touch.position;
                UnityEngine.Vector3 position = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(pos.x, pos.y, transform.position.z - Camera.main.transform.position.z));

                //position the player
                if (transform.position != position)
                    transform.position = UnityEngine.Vector3.Lerp(transform.position, position, Speed * Time.deltaTime);
            }
        }
    }
    void PlayerLook()
    {
        if (Target != null)
        {
            UnityEngine.Vector2 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, angle * -1)), Speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, Angle)), Speed * Time.deltaTime);
        }
    }
    void PlayerShot()
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
