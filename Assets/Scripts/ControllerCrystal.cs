using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ControllerCrystal : MonoBehaviour
{
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    float ExploedInSec = 3.0f;
    Transform MoveTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionNow());
    }
    void Update()
    {
        if (MoveTarget != null) transform.position = Vector3.MoveTowards(transform.position, MoveTarget.position, 1.0f * Time.deltaTime);
    }
    IEnumerator ExplosionNow() 
    {
        yield return new WaitForSeconds(ExploedInSec);
        GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
    public void CrystalTarget(Transform target)
    {
        MoveTarget = target;
    }
}
