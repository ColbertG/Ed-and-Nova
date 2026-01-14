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
    List<Text> PointCount;
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
    List<Button> ActiveButton;
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
    int PlayerRPStart = 100000;
    int PlayerHPStart = 0;
    bool Pause = false;
    int pickShipActive = 0;
    public static bool PlayerBarrierActive = false;
    // Start is called before the first frame update
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("playerHp", 0);
        PlayerPrefs.SetInt("playerDp", 0);
        PlayerPrefs.SetInt("rocketHp", 0);
        PlayerPrefs.SetInt("rocketDp", 0);
        PlayerPrefs.SetInt("playerSpeed", 0);

        PlayerPrefs.SetInt("playerHpLevel", 0);
        PlayerPrefs.SetInt("playerDpLevel", 0);
        PlayerPrefs.SetInt("rocketLevel", 0);
        PlayerPrefs.SetInt("playerSpeedLevel", 0);

        PlayerPrefs.SetInt("playerRp", 0);

        PlayerPrefs.SetInt("scoreKeeper", 0);
    }
    void Start()
    {
        ControllerMenus[0].OpenMenu();
    }

    // Update is called once per frame
    void Update()
    {
        PointCount[0].text = PlayerPrefs.GetInt("scoreKeeper", 0).ToString("00000000000");
        PointCount[1].text = PlayerPrefs.GetInt("playerRp", 0).ToString("00000000000");

        PointCount[2].text = PlayerPrefs.GetInt("playerHpLevel", 0).ToString("000"); 
        PointCount[3].text = PlayerPrefs.GetInt("playerDpLevel", 0).ToString("000");
        PointCount[4].text = PlayerPrefs.GetInt("rocketLevel", 0).ToString("000");
        PointCount[5].text = PlayerPrefs.GetInt("playerSpeedLevel", 0).ToString("000");

        if (DialogDone) 
        {
            if (StartGameNow)
            {
                if (LevelCount == 1 && !LevelComplete) Level1();
                if (LevelCount == 2 && !LevelComplete) Level2();
                if (LevelCount == 3 && !LevelComplete) Level3();
                if (LevelCount == 4 && !LevelComplete) Level4();
                if (LevelCount == 5 && !LevelComplete) Level5();
                if (LevelCount == 6 && !LevelComplete) Level6();
                if (LevelCount == 7 && !LevelComplete) Level7();
                if (LevelCount == 8 && !LevelComplete) Level8();
                CheckPlayerHealth();
                if (PlayerHP <= 0) EndGame();
                if (PlayerHP > 0) SpawnMeteors.CrystalTarget(Player.gameObject.transform);
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
        if (PlayerRP < 20000) return;
        else if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-20000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 20000);
        }

        int level = PlayerPrefs.GetInt("playerHpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerHpLevel", level);

        int newHP = PlayerPrefs.GetInt("playerHp", 0) + 10;
        PlayerPrefs.SetInt("playerHp", newHP); 
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints(10);
    }
    public void DpUpgrade()
    {
        if (PlayerRP < 10000) return;
        else if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-10000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else 
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 10000);
        }

        int level = PlayerPrefs.GetInt("playerDpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerDpLevel", level);

        int newDP = PlayerPrefs.GetInt("playerDp", 0) + 1;
        PlayerPrefs.SetInt("playerDp", newDP);
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ColliderPlayer>().DestructionPoints(1);
    }
    public void RocketUpgrade()
    {
        if (PlayerRP < 15000) return;
        else if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-15000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 15000);
        }

        int level = PlayerPrefs.GetInt("rocketLevel", 0) + 1;
        PlayerPrefs.SetInt("rocketLevel", level);

        int newHP = PlayerPrefs.GetInt("rocketHp", 0) + 1;
        int newDP = PlayerPrefs.GetInt("rocketDp", 0) + 1;
        PlayerPrefs.SetInt("rocketHp", newHP);
        PlayerPrefs.SetInt("rocketDp", newDP);
    }
    public void SpeedUpgrade()
    {
        if (PlayerRP < 5000) return;
        else if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-5000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 5000);
        }

        int level = PlayerPrefs.GetInt("playerSpeedLevel", 0) + 1;
        PlayerPrefs.SetInt("playerSpeedLevel", level);

        int newSpeed = PlayerPrefs.GetInt("playerSpeed", 0) + 1;
        PlayerPrefs.SetInt("playerSpeed", newSpeed);
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ControllerPlayer>().SetSpeed(1);
    }

    public void UpgradeMenu() 
    {
        ControllerMenus[1].CloseMenu();

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

        LevelCount = 1;
        LevelSetUpDone = false;
        LevelComplete = false;
        StartGameNow = true;

        DialogDone = false;

        ControllerMenus[3].OpenMenu();
        DialogReset();

        PlayerPrefs.SetInt("scoreKeeper", 0);
    }
    public void NextLevel()
    {
        LevelCount++;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        ActiveButton[0].gameObject.SetActive(false);

        ControllerMenus[5].CloseMenu();

        ControllerMenus[2].CloseMenu();

        ControllerMenus[3].OpenMenu();

    }
    public void ResetLevel()
    {
        ActiveButton[1].gameObject.SetActive(false);

        ControllerMenus[1].CloseMenu();

        ControllerMenus[5].CloseMenu();

        DialogReset();
        ControllerMenus[3].OpenMenu();
        
        LevelCount = LevelCount;
        LevelSetUpDone = false;
        LevelComplete = false;
        StartGameNow = true;
        
        DialogDone = false;

        PlayerPrefs.SetInt("scoreKeeper", 0);
    }
    public void PickLevel() 
    {
        ControllerMenus[0].CloseMenu();

        ControllerMenus[6].OpenMenu();
    }
    public void MainMenu() 
    {
        ControllerMenus[0].OpenMenu();
        ControllerMenus[1].CloseMenu();
        ControllerMenus[2].CloseMenu();
        ControllerMenus[5].CloseMenu();
        ControllerMenus[6].CloseMenu();
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
            Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(PlayerPrefs.GetInt("playerRp", 0));
            Player.gameObject.GetComponent<ControllerPlayer>().SetSpeed(PlayerPrefs.GetInt("playerSpeed", 0));

            PlayerHP = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
            PlayerHPStart = PlayerHP;
        }
        ControllerMenus[4].OpenMenu();
    }
    void MenuSetUp() 
    {
        LevelComplete = true;
        if (PlayerHP > 0)
        {
            ActiveButton[1].gameObject.SetActive(false);
            ActiveButton[0].gameObject.SetActive(true);
            ControllerMenus[2].OpenMenu();
        }
        else 
        {
            ActiveButton[0].gameObject.SetActive(false);
            ActiveButton[1].gameObject.SetActive(true);
            ControllerMenus[1].OpenMenu();
        } 
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
                PlayerPrefs.SetInt("playerRp", PlayerRP);
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
    public void Start1()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 1;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
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
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 100 && !LevelSpawnMeteorsDone))
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
    public void Start2()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 2;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
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
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 150 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnBombs.SpawnCounter() >= 125 && !LevelSpawnBombsDone))
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
    public void Start3() 
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 3;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
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
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 150 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 125 && !LevelSpawnEnemiesDone))
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
    public void Start4()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 4;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
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
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 200 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 175 && !LevelSpawnEnemiesDone))
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
    public void Start5()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 5;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
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
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 250 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 225 && !LevelSpawnEnemiesDone))
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
    public void Start6()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 6;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level6()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 6 start");

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
        SpawnEnemies.SpawnLevel(2, 1);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(4);
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 300 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 275 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 6 Done");
        }
    }
    public void Start7()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 7;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level7()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 7 start");

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = true;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);
            SpawnBombs.SpawnCounter(true);

            SpawnBarriers.SpawnLevel(0, 3);
        }
        SpawnMeteors.SpawnLevel(2);
        SpawnEnemies.SpawnLevel(2, 1);
        SpawnBombs.SpawnLevel(1);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(4);
        SpawnBombs.FaceingBomb(4);
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 350 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 325 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (PlayerHP <= 0 || (SpawnBombs.SpawnCounter() >= 325 && !LevelSpawnBombsDone))
        {
            SpawnBombs.SpawnCounter(true);
            SpawnBombs.enabled = false;
            LevelSpawnBombsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && SpawnBombs.BombDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone && LevelSpawnBombsDone)
        {
            MenuSetUp();
            Debug.Log("Level 7 Done");
        }
    }
    public void Start8()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 8;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level8()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 8 start");

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
        SpawnMeteors.SpawnLevel(3);
        SpawnEnemies.SpawnLevel(3, 2);
        SpawnMeteors.FaceingMeteor(0);
        SpawnEnemies.FaceingEnemy(0);
        Player.AngleControll(90);
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 100 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 125 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 8 Done");
        }
    }
}
