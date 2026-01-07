using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    [SerializeField]
    Slider PlayerHPBar;
    [SerializeField] 
    Slider PlayerRPBar;
    [SerializeField]
    SpawnPlayer SpawnPlayers;
    ControllerPlayer Player;
    [SerializeField]
    List<ControllerBoss> Bosses;
    [SerializeField]
    SpawnMeteor SpawnMeteors;
    [SerializeField]
    SpawnEnemy SpawnEnemies;
    [SerializeField]
    SpawnBomb SpawnBombs;
    [SerializeField]
    SpawnBarrier SpawnBarriers;
    [SerializeField]
    List<ControllerMenu> ControllerMenus;
    [SerializeField]
    ControllerDialog ControllerDialogs;
    bool StartGameNow = false;
    bool DialogDone = false;
    bool LevelComplete = false;
    bool LevelSetUpDone = false;
    bool LevelSpawnMeteorsDone = false;
    bool LevelSpawnEnemiesDone = false;
    bool LevelSpawnBombsDone = false;
    public int LevelCount { get; private set; } = 1;
    int PlayerHP = 0;
    int PlayerRP = 0;
    int PlayerRPStart = 0;
    int PlayerHPStart = 0;
    bool Pause = false;
    int pickShipActive = 0;
    // Start is called before the first frame update
    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt("playerHp", 0);
        //PlayerPrefs.SetInt("playerDp", 0);
        //PlayerPrefs.SetInt("rocketHp", 0);
        //PlayerPrefs.SetInt("rocketDp", 0);
        //PlayerPrefs.SetInt("playerSpeed", 0);

        //PlayerPrefs.SetInt("playerHpLevel", 0);
        //PlayerPrefs.SetInt("playerDpLevel", 0);
        //PlayerPrefs.SetInt("rocketLevel", 0);
        //PlayerPrefs.SetInt("playerSpeedLevel", 0);
    }
    void Start()
    {
        ControllerMenus[0].OpenMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogDone) 
        {
            if (StartGameNow)
            {
                if (LevelCount == 1 && !LevelComplete) Level1();
                if (LevelCount == 2 && !LevelComplete) Level2();
                if (LevelCount == 3 && !LevelComplete) Level3();
                if (LevelCount == 4 && !LevelComplete) Level4();
                if (LevelCount == 5 && !LevelComplete) Level5();
                CheckPlayerHealth();
                if (PlayerHP <= 0) EndGame();
            }
        }

        //Player.SetTarget(null);

        //SpawnMeteors.FaceingMeteor(1);
        //SpawnEnemies.FaceingEnemy(1);

        //if(clone == null)
        //    clone = Instantiate(Bosses[0].gameObject, transform.position, transform.rotation) as GameObject;
        //if (clone != null) 
        //{
        //    Player.SetTarget(clone.transform);
        //    clone.GetComponent<ControllerBoss>().SetTarget(Player.gameObject.transform);
        //    SpawnMeteors.FaceingMeteor(clone.GetComponent<ControllerBoss>().PickSpot());
        //    SpawnEnemies.FaceingEnemy(clone.GetComponent<ControllerBoss>().PickSpot());
        //    clone.GetComponent<SpriteRenderer>().color = Color.black;
        //    if(clone.GetComponent<SpriteRenderer>().color == Color.black)
        //     Player.SetTarget(null);
        //    else if(clone.GetComponent<SpriteRenderer>().color == Color.white)
        //        Player.SetTarget(clone.transform);
        //    //clone.GetComponent<SpriteRenderer>().color = Color.white;
        //}


        //Player.AngleControll(SpawnMeteors.FaceingMeteor());
        //Player.AngleControll(SpawnEnemies.FaceingEnemy());
    }
    public void HpUpgrade() 
    {
        int level = PlayerPrefs.GetInt("playerHpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerHpLevel", level);

        int newHP = PlayerPrefs.GetInt("playerHp", 0) + 10;
        PlayerPrefs.SetInt("playerHp", newHP);
        Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints(10);
    }
    public void DpUpgrade()
    {
        int level = PlayerPrefs.GetInt("playerDpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerDpLevel", level);

        int newDP = PlayerPrefs.GetInt("playerDp", 0) + 1;
        PlayerPrefs.SetInt("playerDp", newDP);
        Player.gameObject.GetComponent<ColliderPlayer>().DestructionPoints(1);
    }
    public void RocketUpgrade()
    {
        int level = PlayerPrefs.GetInt("rocketLevel", 0) + 1;
        PlayerPrefs.SetInt("rocketLevel", level);

        int newHP = PlayerPrefs.GetInt("rocketHp", 0) + 1;
        int newDP = PlayerPrefs.GetInt("rocketDp", 0) + 1;
        PlayerPrefs.SetInt("rocketHp", newHP);
        PlayerPrefs.SetInt("rocketDp", newDP);
    }
    public void SpeedUpgrade()
    {
        int level = PlayerPrefs.GetInt("playerSpeedLevel", 0) + 1;
        PlayerPrefs.SetInt("playerSpeedLevel", level);

        int newSpeed = PlayerPrefs.GetInt("playerSpeed", 0) + 1;
        PlayerPrefs.SetInt("playerSpeed", newSpeed);
        Player.gameObject.GetComponent<ControllerPlayer>().SetSpeed(1);
    }
    public void UpgradeMenu() 
    {
        ControllerMenus[2].CloseMenu();

        ControllerMenus[5].OpenMenu();
    }
    public void PauseResumeGame() 
    {
        Pause = !Pause;
        if (Pause) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
    }
    public void NextDialog() 
    {
        int onCount = ControllerDialogs.ShowNextDialog();
        if (onCount == 6) 
        {
            ControllerMenus[3].CloseMenu();
            DialogDone = true;
        }
        if (onCount == 10) 
        {
            ControllerMenus[3].CloseMenu();
            DialogDone = true;
        }
        if (onCount == 15)
        {
            ControllerMenus[3].CloseMenu();
            //Rework When Dialog is done
            ControllerDialogs.ShowNextDialog(onCount--);
            DialogDone = true;
        }
        ControllerDialogs.ShowDialog();
    }
    void DialogReset() 
    {
        if (LevelCount == 1) ControllerDialogs.ShowNextDialog(0);
        if (LevelCount == 2) ControllerDialogs.ShowNextDialog(7);
        if (LevelCount == 3) ControllerDialogs.ShowNextDialog(11);
        ControllerDialogs.ShowDialog();
    }
    public void StartGame() 
    {
        ControllerMenus[0].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();
        
        StartGameNow = true;
    }
    public void NextLevel()
    {
        LevelCount++;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        ControllerMenus[5].CloseMenu();

        ControllerMenus[2].CloseMenu();

        ControllerMenus[3].OpenMenu();

    }
    public void ResetLevel()
    {
        ControllerMenus[1].CloseMenu();
        
        DialogReset();
        ControllerMenus[3].OpenMenu();
        
        LevelCount = LevelCount;
        LevelSetUpDone = false;
        LevelComplete = false;
        StartGameNow = true;
        
        DialogDone = false;
    }
    void PlayerReset() 
    {

        int pickShip = 0;

        int upgeade1 = PlayerPrefs.GetInt("playerHpLevel", 0);
        int upgrade2 = PlayerPrefs.GetInt("playerDpLevel", 0);
        int upgrade3 = PlayerPrefs.GetInt("rocketLevel", 0);
        int upgrade4 = PlayerPrefs.GetInt("playerSpeedLevel", 0);

        bool ship1 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 20 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 0;
        bool ship2 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 40 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 20;
        bool ship3 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 60 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 40;
        bool ship4 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 80 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 60;
        bool ship5 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 80;

        if (ship1) pickShip = 0;
        if (ship2) pickShip = 1;
        if (ship3) pickShip = 2;
        if (ship4) pickShip = 3;
        if (ship5) pickShip = 4;

        if (PlayerHP <= 0 || pickShipActive != pickShip) 
        {
            if (pickShipActive != pickShip && Player != null) Destroy(Player.gameObject);
            pickShipActive = pickShip;

            SpawnPlayers.SpawnLevel(pickShip, 1);

            Player = SpawnPlayers.ActivePlayer().GetComponent<ControllerPlayer>();

            Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints(PlayerPrefs.GetInt("playerHp", 0));
            Player.gameObject.GetComponent<ColliderPlayer>().DestructionPoints(PlayerPrefs.GetInt("playerDp", 0));
            Player.gameObject.GetComponent<ControllerPlayer>().SetSpeed(PlayerPrefs.GetInt("playerSpeed", 0));

            PlayerHP = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
            PlayerHPStart = PlayerHP;
            PlayerRPStart = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints();
        }
        ControllerMenus[4].OpenMenu();
    }
    void MenuSetUp() 
    {
        LevelComplete = true;
        if (PlayerHP > 0) ControllerMenus[2].OpenMenu();
        else ControllerMenus[1].OpenMenu();
        ControllerMenus[4].CloseMenu();
    }
    void CheckPlayerHealth()
    {
        if (Player != null)
        {
            if (Player.gameObject.GetComponent<ColliderPlayer>() != null) 
            {
                if (Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints() > PlayerHPStart)
                    PlayerHPStart = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
                PlayerHP = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
                PlayerHPBar.value = (float)PlayerHP / PlayerHPStart;

                if (Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints() > PlayerRPStart)
                    PlayerRPStart = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints();
                PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints();
                PlayerRPBar.value = (float)Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints() / PlayerRPStart;
            }
        }
        else PlayerHP = 0;
    }
    void EndGame() 
    {
        SpawnMeteors.SpawnRemover();
        SpawnBombs.SpawnRemover();
        SpawnEnemies.SpawnRemover();
        SpawnBarriers.SpawnRemover();
    }
    void Level1() 
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 1 start");
            
            PlayerReset();

            Player.SetTarget(null);
            
            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false; 
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);

            SpawnBarriers.SpawnLevel(0,1);
        }
        SpawnMeteors.SpawnLevel(1);
        SpawnMeteors.FaceingMeteor(2);
        Player.AngleControll(SpawnMeteors.FaceingMeteor());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 10 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            MenuSetUp();
            Debug.Log("Level 1 Done");
        }
    }
    void Level2() 
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 2 start");
            
            PlayerReset();
            
            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = true;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false; 
            
            SpawnMeteors.SpawnCounter(true); 
            SpawnBombs.SpawnCounter(true);

            SpawnBarriers.SpawnLevel(0, 1);
        }
        SpawnMeteors.SpawnLevel(1);
        SpawnBombs.SpawnLevel(1);
        SpawnMeteors.FaceingMeteor(2);
        SpawnBombs.FaceingBomb(2);
        Player.AngleControll(SpawnBombs.FaceingBomb());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 15 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnBombs.SpawnCounter() >= 15 && !LevelSpawnBombsDone))
        {
            SpawnBombs.SpawnCounter(true);
            SpawnBombs.enabled = false;
            LevelSpawnBombsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnBombs.BombDone() && LevelSpawnMeteorsDone && LevelSpawnBombsDone)
        {
            MenuSetUp();
            Debug.Log("Level 2 Done");
        }
    }
    void Level3()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 3 start");

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        } 
        SpawnMeteors.SpawnLevel(2);
        SpawnEnemies.SpawnLevel(1);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(2);
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 15 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 15 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 3 Done");
        }
    }
    void Level4()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 4 start");

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(2);
        SpawnEnemies.SpawnLevel(1);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(3);
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 20 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 20 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 4 Done");
        }
    }
    void Level5()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 5 start");

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(2);
        SpawnEnemies.SpawnLevel(2);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(4);
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 25 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 25 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 5 Done");
        }
    }
}
