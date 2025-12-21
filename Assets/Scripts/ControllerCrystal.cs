using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCrystal : MonoBehaviour
{
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    float ExploedInSec = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionNow());
    }
    IEnumerator ExplosionNow() 
    {
        yield return new WaitForSeconds(ExploedInSec);
        GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
