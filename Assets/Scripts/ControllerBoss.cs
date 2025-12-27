using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
class Rockets
{
    public float FireRate = 0.5f;
    public GameObject MainRocket;
    public Transform SpawnPoint;
    public float NextFireTime = 0f;
}
[Serializable]
class ControllAnimator 
{
    public Animator Animators;
    public List<String> AniamName;
}
public class ControllerBoss : MonoBehaviour
{
    [SerializeField]
    float Speed = 1;
    [SerializeField]
    Transform Target;
    [SerializeField]
    List<Rockets> Rocket;
    [SerializeField]
    ControllAnimator ControllAnima;
    [SerializeField]
    float MoveRateMin = 0.5f;
    [SerializeField]
    float MoveRateMax = 0.5f;
    AnimatorStateInfo AnimStateInfo;
    bool SpotsDone = false;
    bool laserDone = true;
    float MoveRate;
    float NextMoveTime = 0f;
    Vector3[] Spots = new Vector3 [5];
    Vector3[] SpotsBF = new Vector3[3];
    int pick = 2;
    GameObject clone3;
    void Awake()
    {
        SpotBoss();
        SpotBossBF();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Spots[pick];
    }
    // Update is called once per frame
    void Update()
    {
        if(ControllAnima.Animators != null)
        AnimStateInfo = ControllAnima.Animators.GetCurrentAnimatorStateInfo(0);
        AnimatorControll();
        MovementBoss();
        LookAt();
    }
    void LateUpdate()
    {
        SpotBoss();
    }

    void SpotBoss() 
    {
        float width = Screen.width;
        float height = Screen.height;
        Spots[0] = Camera.main.ScreenToWorldPoint(new Vector3(width - 25, height / 2, transform.position.z - Camera.main.transform.position.z));
        Spots[1] = Camera.main.ScreenToWorldPoint(new Vector3(width - 25, height - 25, transform.position.z - Camera.main.transform.position.z));
        Spots[2] = Camera.main.ScreenToWorldPoint(new Vector3(width / 2, height - 25, transform.position.z - Camera.main.transform.position.z));
        Spots[3] = Camera.main.ScreenToWorldPoint(new Vector3((width / width) + 25, height - 25, transform.position.z - Camera.main.transform.position.z));
        Spots[4] = Camera.main.ScreenToWorldPoint(new Vector3((width / width) + 25, height / 2, transform.position.z - Camera.main.transform.position.z));
    }
    void SpotBossBF()
    {
        float width = Screen.width;
        float height = Screen.height;
        Vector3 y = Camera.main.ScreenToWorldPoint(new Vector3((width / width) + 25, (height / height) + 25, transform.position.z - Camera.main.transform.position.z));
        SpotsBF[0] = new Vector3(Spots[0].x, UnityEngine.Random.Range(y.y, Spots[1].y), 0);
        SpotsBF[1] = new Vector3(UnityEngine.Random.Range(Spots[3].x, Spots[0].x), Spots[1].y, 0);
        SpotsBF[2] = new Vector3(Spots[3].x, UnityEngine.Random.Range(y.y, Spots[1].y), 0);

    }
    void AnimatorControll() 
    {
        if (Rocket.Count >= 1 && Time.time >= Rocket[0].NextFireTime)
        {
            GameObject clone1 = Instantiate(Rocket[0].MainRocket, Rocket[0].SpawnPoint.position, Rocket[0].SpawnPoint.rotation) as GameObject;
            Rocket[0].NextFireTime = Time.time + Rocket[0].FireRate;
        }
        if (Rocket.Count >= 2 && Time.time >= Rocket[1].NextFireTime)
        {
            GameObject clone2 = Instantiate(Rocket[1].MainRocket, Rocket[1].SpawnPoint.position, Rocket[1].SpawnPoint.rotation) as GameObject;
            Rocket[1].NextFireTime = Time.time + Rocket[1].FireRate;
        }
        if (ControllAnima.Animators != null)
        {
            if (Rocket.Count >= 3 && Time.time >= (Rocket[2].NextFireTime + Rocket[2].FireRate) && laserDone)
            {
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[4], false);
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[0], true);
                if (AnimStateInfo.normalizedTime >= 1.0f && AnimStateInfo.IsName(ControllAnima.AniamName[1]))
                {
                    laserDone = false;
                    ControllAnima.Animators.SetBool(ControllAnima.AniamName[0], false);
                    ControllAnima.Animators.SetBool(ControllAnima.AniamName[2], true);
                    clone3 = Instantiate(Rocket[2].MainRocket, Rocket[2].SpawnPoint.position, Rocket[2].SpawnPoint.rotation) as GameObject;
                }
            }
            if (AnimStateInfo.normalizedTime >= 15.0f && AnimStateInfo.IsName(ControllAnima.AniamName[3]) && laserDone == false)
            {
                Destroy(clone3);
                laserDone = true;
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[2], false);
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[4], true);
                Rocket[2].NextFireTime = Time.time + Rocket[2].FireRate;
            }
            else
            {
                if (clone3 != null)
                {
                    clone3.transform.position = Rocket[2].SpawnPoint.position;
                    clone3.transform.rotation = transform.rotation;
                }
            }
        }
        else 
        {
            if (Rocket.Count >= 3 && Time.time >= Rocket[2].NextFireTime)
            {
                GameObject clone3 = Instantiate(Rocket[2].MainRocket, Rocket[2].SpawnPoint.position, Rocket[2].SpawnPoint.rotation) as GameObject;
                Rocket[2].NextFireTime = Time.time + Rocket[2].FireRate;
            }
            if (Rocket.Count >= 4 && Time.time >= Rocket[3].NextFireTime)
            {
                GameObject clone4 = Instantiate(Rocket[3].MainRocket, Rocket[3].SpawnPoint.position, Rocket[3].SpawnPoint.rotation) as GameObject;
                Rocket[3].NextFireTime = Time.time + Rocket[3].FireRate;
            }
        }
    }
    void MovementBoss() 
    {
        if ((string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", Spots[pick].magnitude)) && !SpotsDone)
        {
            transform.position = Vector3.Lerp(transform.position, Spots[pick], Speed * Time.deltaTime);
            //Debug.Log("moving " + Mathf.Round(transform.position.magnitude) + " --> " + Mathf.Round(Spots[pick].magnitude));
        }
        else
        {
            if (Time.time >= NextMoveTime)
            {
                pick = UnityEngine.Random.Range(0, 5);
                MoveRate = UnityEngine.Random.Range(MoveRateMin, MoveRateMax);
                NextMoveTime = Time.time + MoveRate;
                SpotsDone = false;
            }
            else 
            {
                SpotsDone = true;
                if (pick == 0) 
                {
                    if (string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", SpotsBF[0].magnitude))
                    {
                        transform.position = Vector3.Lerp(transform.position, SpotsBF[0], Speed * Time.deltaTime);
                    }
                    else SpotBossBF();
                }
                if (pick == 2) 
                {
                    if (string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", SpotsBF[1].magnitude))
                    {
                        transform.position = Vector3.Lerp(transform.position, SpotsBF[1], Speed * Time.deltaTime);
                    }
                    else SpotBossBF();
                }
                if (pick == 4) 
                {
                    if (string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", SpotsBF[2].magnitude))
                    {
                        transform.position = Vector3.Lerp(transform.position, SpotsBF[2], Speed * Time.deltaTime);
                    }
                    else SpotBossBF();
                }

            }
            //Debug.Log("pick");
        }
    }
    void LookAt() 
    {
        if (Target != null)
        {
            Vector2 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle * -1)), Speed * Time.deltaTime);
        }
    }
    public int PickSpot() 
    {
        return pick;
    }
    public void SetTarget(Transform target) 
    {
        Target = target;
    }
}
