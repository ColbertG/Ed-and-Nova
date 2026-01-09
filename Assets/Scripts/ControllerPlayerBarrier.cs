using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerBarrier : MonoBehaviour
{
    [SerializeField]
    float RemoveInSec = 3.0f;
    [SerializeField]
    int DP = 5;
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
        ControllerGame.PlayerBarrierActive = false;
        Destroy(gameObject);
    }
    public void MovingTarget(Transform target)
    {
        MoveTarget = target;
    }
    public int DestructionPoints(int dp = 0)
    {
        DP = DP + dp;
        return DP;
    }
}
