using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMeteor : MonoBehaviour
{
    [SerializeField]
    int Score = 1;
    [SerializeField]
    int DP = 1;
    [SerializeField]
    int HP = 1;
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    GameObject Crystal;
    Transform Target;
    bool Exploed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderPlayerCpu>() != null)
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ControllerPlayerBarrier>() != null)
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderBarrier>() != null)
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderLaser>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderLaser>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null) 
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderBoss>() != null) 
        {
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
        {
            HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
            if (HP <= 0) Exploed = true;
        }
        if (Exploed) 
        {
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            GameObject clone2 = Instantiate(Crystal, transform.position, transform.rotation) as GameObject;
            clone2.GetComponent<ControllerCrystal>().CrystalTarget(Target);
            Exploed = false;
            Destroy(gameObject);
        }
    }
    public int DestructionPoints()
    {
        return DP;
    }
    public int ScorePoints() 
    {
        return Score;
    }
    public void MeteorTarget(Transform target) 
    {
        Target = target;
    }
}
