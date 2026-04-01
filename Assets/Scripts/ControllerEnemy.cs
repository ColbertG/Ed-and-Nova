using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEnemy : MonoBehaviour
{
    [SerializeField]
    Transform MoveTarget;
    [SerializeField]
    float Speed = 1;
    [SerializeField]
    List<Rockets> Rocket;
    [SerializeField]
    float InvisibleTime = 0.0f;
    [SerializeField]
    float InvisibleTimePause = 0.0f;
    bool InvisibleActive = false;
    float InvisiblePauseTime = 0.0f;
    float TimeInvisible = 0.0f;
    Transform Target;
    bool TargetDone = false;
    float HoldLookforSec = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        OffScreen();
        RocketShot();
        LookAt();
        if (TargetDone) 
        {
            StartCoroutine(PlayerLookHold());
            TargetDone = false;
        }
        if (!InvisibleActive && InvisibleTime != 0)
        {
            if (Time.time > InvisiblePauseTime)
            {
                TimeInvisible = UnityEngine.Random.Range(2.0f, InvisibleTime);
                StartCoroutine(InvisibleNow());
            }
        }
    }

    IEnumerator InvisibleNow()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        InvisibleActive = true;
        yield return new WaitForSeconds(TimeInvisible);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        InvisiblePauseTime = Time.time + UnityEngine.Random.Range(5.0f, InvisibleTimePause);
        InvisibleActive = false;
    }
    void OffScreen() 
    {
        float width = Screen.width;
        float height = Screen.height;
        UnityEngine.Vector3 pos = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width, height, 1));
        UnityEngine.Vector3 pos2 = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width / width, height / height, 1));
        if (MoveTarget != null) transform.position = UnityEngine.Vector3.MoveTowards(transform.position, MoveTarget.position, Speed * Time.deltaTime);
        if ((transform.position.x + 1) < pos2.x || (transform.position.x - 1) > pos.x) Destroy(gameObject);
        if ((transform.position.y + 1) < pos2.y || (transform.position.y - 1) > pos.y) Destroy(gameObject);
    }
    void RocketShot() 
    {
        for (int i = 0; i < Rocket.Count; i++) 
        {

            if (Time.time >= Rocket[i].NextFireTime)
            {
                GameObject clone = Instantiate(Rocket[i].MainRocket, Rocket[i].SpawnPoint.position, Rocket[i].SpawnPoint.rotation) as GameObject;
                Rocket[i].NextFireTime = Time.time + Rocket[i].FireRate;
            }
        }
    }
    void LookAt()
    {
        if (Target != null)
        {
            UnityEngine.Vector2 dir = Target.position - transform.position;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = UnityEngine.Quaternion.Slerp(transform.rotation, UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(0, 0, angle * -1)), Speed * Time.deltaTime);
        }
    }
    public void SetTarget(Transform target, float forSec)
    {
        HoldLookforSec = forSec;
        TargetDone = true;
        Target = target;
    }
    IEnumerator PlayerLookHold()
    {
        yield return new WaitForSeconds(HoldLookforSec);
        Target = null;
    }
}
