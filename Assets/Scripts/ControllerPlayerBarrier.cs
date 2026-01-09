using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerBarrier : MonoBehaviour
{
    [SerializeField]
    float RemoveInSec = 3.0f;
    Transform MoveTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionNow());
    }
    void Update()
    {
        if (MoveTarget != null) transform.position = MoveTarget.position;
    }
    IEnumerator ExplosionNow()
    {
        yield return new WaitForSeconds(RemoveInSec);
        Destroy(gameObject);
    }
    public void MovingTarget(Transform target)
    {
        MoveTarget = target;
    }
}
