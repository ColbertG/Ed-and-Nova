using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBarrier : MonoBehaviour
{
    [SerializeField]
    GameObject Explosion;
    bool Exploed = false;
    private void OnTriggerEnter2D(Collider2D collision) 
    { 
        if (collision.gameObject.GetComponent<ColliderMeteor>() != null) 
        {
            if (collision.gameObject.GetComponent<ColliderMeteor>().DestructionPoints() > 1) 
            {
                if(gameObject.transform.localScale.y > 0)
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 0.025f, gameObject.transform.localScale.z);
            }
            Exploed = true;
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
        {
            if (collision.gameObject.CompareTag("Enemy")) 
            {
                if (collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints() > 1)
                {
                    if (gameObject.transform.localScale.y > 0)
                        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 0.025f, gameObject.transform.localScale.z);
                }
                Exploed = true;
            }
        }
        if (collision.gameObject.GetComponent<ColliderEnemy>() != null)
        {
            if (collision.gameObject.GetComponent<ColliderEnemy>().DestructionPoints() > 1)
            {
                if (gameObject.transform.localScale.y > 0)
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 0.025f, gameObject.transform.localScale.z);    
            }
            Exploed = true;
        }

        if (collision.gameObject.GetComponent<ColliderBomb>() != null)
        {
            if (collision.gameObject.GetComponent<ColliderBomb>().DestructionPoints() > 1)
            {
                if (gameObject.transform.localScale.y > 0)
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 0.025f, gameObject.transform.localScale.z);
            }
            Exploed = true;
        }
        if (Exploed)
        {
            GameObject clone = Instantiate(Explosion, collision.transform.position, transform.rotation) as GameObject;
            Exploed = false;
        }
    }
}
