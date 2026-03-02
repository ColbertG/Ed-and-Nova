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
    int LaserIndex = -1;
    [SerializeField]
    ControllAnimator ControllAnima;
    [SerializeField]
    float MoveRateMin = 0.5f;
    [SerializeField]
    float MoveRateMax = 0.5f;
    [SerializeField]
    float InvisibleTime = 0.0f;
    [SerializeField]
    float InvisibleTimePause = 0.0f;
    AnimatorStateInfo AnimStateInfo;
    bool SpotsDone = false;
    bool laserDone = true;
    float MoveRate;
    float NextMoveTime = 0f;
    Vector3[] Spots = new Vector3 [5];
    Vector3[] SpotsBF = new Vector3[3];
    int pick = 2;
    GameObject clone3;
    bool InvisibleActive = false;
    float InvisiblePauseTime = 0.0f;
    float TimeInvisible = 0.0f;
    void Awake()
    {
        SpotBoss();
        SpotBossSideToSide();
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
        if (!InvisibleActive && InvisibleTime != 0)
        {
            if (Time.time > InvisiblePauseTime) 
            {
                TimeInvisible = UnityEngine.Random.Range(5.0f, InvisibleTime);
                StartCoroutine(InvisibleNow());
            }
        }
    }
    void LateUpdate()
    {
        SpotBoss();
    }
    IEnumerator InvisibleNow()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        InvisibleActive = true;
        yield return new WaitForSeconds(TimeInvisible);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        InvisiblePauseTime = Time.time + UnityEngine.Random.Range(5.0f, InvisibleTimePause);
        InvisibleActive = false;
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
    void SpotBossSideToSide()
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
        for (int i = 0; i < Rocket.Count; i++)
        {
            if (i == LaserIndex) continue;
            if (Time.time >= Rocket[i].NextFireTime)
            {
                GameObject clone = Instantiate(Rocket[i].MainRocket, Rocket[i].SpawnPoint.position, Rocket[i].SpawnPoint.rotation) as GameObject;
                Rocket[i].NextFireTime = Time.time + Rocket[i].FireRate;
            }
        }
        if (ControllAnima.Animators != null)
        {
            if (Time.time >= (Rocket[LaserIndex].NextFireTime + Rocket[LaserIndex].FireRate) && laserDone)
            {
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[4], false);
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[0], true);
                if (AnimStateInfo.normalizedTime >= 1.0f && AnimStateInfo.IsName(ControllAnima.AniamName[1]))
                {
                    laserDone = false;
                    ControllAnima.Animators.SetBool(ControllAnima.AniamName[0], false);
                    ControllAnima.Animators.SetBool(ControllAnima.AniamName[2], true);
                    clone3 = Instantiate(Rocket[LaserIndex].MainRocket, Rocket[LaserIndex].SpawnPoint.position, Rocket[LaserIndex].SpawnPoint.rotation, transform) as GameObject;
                }
            }
            if (AnimStateInfo.normalizedTime >= 30.0f && AnimStateInfo.IsName(ControllAnima.AniamName[3]) && laserDone == false)
            {
                Destroy(clone3);
                laserDone = true;
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[2], false);
                ControllAnima.Animators.SetBool(ControllAnima.AniamName[4], true);
                Rocket[LaserIndex].NextFireTime = Time.time + Rocket[LaserIndex].FireRate;
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
    }
    void MovementBoss() 
    {
        if ((string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", Spots[pick].magnitude)) && !SpotsDone)
        {
            transform.position = Vector3.Lerp(transform.position, Spots[pick], Speed * Time.deltaTime);
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
                    else SpotBossSideToSide();
                }
                if (pick == 2) 
                {
                    if (string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", SpotsBF[1].magnitude))
                    {
                        transform.position = Vector3.Lerp(transform.position, SpotsBF[1], Speed * Time.deltaTime);
                    }
                    else SpotBossSideToSide();
                }
                if (pick == 4) 
                {
                    if (string.Format("{0:0.00}", transform.position.magnitude) != string.Format("{0:0.00}", SpotsBF[2].magnitude))
                    {
                        transform.position = Vector3.Lerp(transform.position, SpotsBF[2], Speed * Time.deltaTime);
                    }
                    else SpotBossSideToSide();
                }

            }
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
