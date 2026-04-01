using System.Collections;
using UnityEngine;

public class ControllerCrystal : MonoBehaviour
{
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    float ExploedInSec = 3.0f;
    [SerializeField]
    float MoveSpeed = 3.0f;
    Transform MoveTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionNow());
    }
    void Update()
    {
        if (MoveTarget != null) transform.position = Vector3.MoveTowards(transform.position, MoveTarget.position, MoveSpeed * Time.deltaTime);
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
