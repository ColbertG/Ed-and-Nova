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
    bool IsDead = false; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ColliderPlayerCpu>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderPlayerCpu>().DestructionPoints();
        if (collision.gameObject.GetComponent<ColliderPlayer>() != null)
            HP = HP - collision.gameObject.GetComponent<ColliderPlayer>().DestructionPoints();
        if (collision.gameObject.GetComponent<ControllerPlayerBarrier>() != null)
        {
            ControllerSound.Instance.PlayerSheild();
            HP = HP - collision.gameObject.GetComponent<ControllerPlayerBarrier>().DestructionPoints();
        }
        if (collision.gameObject.GetComponent<ColliderRocket>() != null)
            if (collision.gameObject.CompareTag("Player"))
                HP = HP - collision.gameObject.GetComponent<ColliderRocket>().DestructionPoints();
        if (HP <= 0 && !IsDead)
        {
            IsDead = true;
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            PlayerPrefs.SetInt("scoreKeeper", PlayerPrefs.GetInt("scoreKeeper", 0) + ScorePoints());
            PlayerPrefs.SetInt("playerKills", PlayerPrefs.GetInt("playerKills", 0) + 1);
            ControllerSound.Instance.ShipExplosion();
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
