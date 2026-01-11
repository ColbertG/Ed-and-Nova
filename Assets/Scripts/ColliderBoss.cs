using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBoss : MonoBehaviour
{
    [SerializeField]
    int Score = 1;
    [SerializeField]
    int HP = 100;
    [SerializeField]
    int DP = 1;
    [SerializeField]
    GameObject Explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderPlayerCpu>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderPlayerCpu>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderPlayer>().DestructionPoints();
        if (collision.gameObject.GetComponent<ControllerPlayerBarrier>() != null)
            HP = HP - collision.gameObject.GetComponent<ControllerPlayerBarrier>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
            if (collision.gameObject.CompareTag("Player"))
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
        if (HP <= 0)
        {
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
            
    }
    public int HealthPoints(int changeHealth = 0)
    {
        HP = HP + changeHealth;
        return HP;
    }
    public int DestructionPoints()
    {
        return DP;
    }
    public int ScorePoints()
    {
        return Score;
    }
}
