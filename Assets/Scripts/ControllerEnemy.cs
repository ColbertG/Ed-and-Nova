using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ControllerEnemy : MonoBehaviour
{
    [SerializeField]
    Transform MoveTarget;
    [SerializeField]
    float Speed = 1;
    [SerializeField]
    List<Rockets> Rocket; 
    Transform Target;
    bool TargetDone = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayerLookHold());
    }

    // Update is called once per frame
    void Update()
    {
        OffScreen();
        RocketShot();
        LookAt();
    }
    void OffScreen() 
    {
        float width = Screen.width;
        float height = Screen.height;
        UnityEngine.Vector3 pos = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width, height, transform.position.z - Camera.main.transform.position.z));
        UnityEngine.Vector3 pos2 = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector3(width / width, height / height, transform.position.z - Camera.main.transform.position.z));
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
    public void SetTarget(Transform target)
    {
        if (!TargetDone) Target = target;
        else Target = null;
    }
    IEnumerator PlayerLookHold()
    {
        yield return new WaitForSeconds(1.0f);
        TargetDone = true;
    }
}
