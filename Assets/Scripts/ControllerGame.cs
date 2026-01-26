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
    Slider BossHPBar;
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
    [SerializeField]
    List<Button> LevelIcons;
    [SerializeField]
    Sprite LevelIconsPass;
    bool StartGameNow = false;
    bool DialogDone = false;
    bool LevelComplete = false;
    bool LevelSetUpDone = false;
    bool LevelSpawnMeteorsDone = false;
    bool LevelSpawnEnemiesDone = false;
    bool LevelSpawnBombsDone = false;
    bool LevelBossDone = false;
    bool LookingAtHold = false;
    public int LevelCount { get; private set; } = 1;
    int BossHp = 0;
    int PlayerHP = 0;
    int PlayerRP = 0;
    int PlayerRPStart = 100000;
    int PlayerHPStart = 0;
    int BossHPStart = 0;
    bool Pause = false;
    int pickShipActive = 0;
    public static bool PlayerBarrierActive = false;
    GameObject BossClone = null;
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

        //PlayerPrefs.SetInt("playerRp", 0);

        //PlayerPrefs.SetInt("scoreKeeper", 0);

        //PlayerPrefs.SetInt("levelCountOn", 1);
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
                if (LevelCount == 9 && !LevelComplete) Level9();
                if (LevelCount == 10 && !LevelComplete) Level10();
                if (LevelCount == 11 && !LevelComplete) Level11();
                if (LevelCount == 12 && !LevelComplete) Level12();
                CheckPlayerHealth();
                if (PlayerHP <= 0) EndGame();
                if (PlayerHP > 0) SpawnMeteors.CrystalTarget(Player.gameObject.transform);
            }
        }
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

    void DialogEnd(int onCount,int end)
    {
        if (onCount == end)
        {
            ControllerMenus[3].CloseMenu();
            DialogDone = true;
        }
        //remove wheen done with all Dialog
        if(onCount == 55)
            ControllerDialogs.ShowNextDialog(onCount--);
    }
    public void NextDialog() 
    {   int onCount = ControllerDialogs.ShowNextDialog();
        DialogEnd(onCount,6);
        DialogEnd(onCount,10);
        DialogEnd(onCount,15);
        DialogEnd(onCount,20);
        DialogEnd(onCount,25);
        DialogEnd(onCount, 30);
        DialogEnd(onCount, 34);
        DialogEnd(onCount, 41);
        DialogEnd(onCount, 45);
        DialogEnd(onCount, 47);
        DialogEnd(onCount, 51);
        DialogEnd(onCount, 55);
        ControllerDialogs.ShowDialog();
    }
    void DialogReset() 
    {
        if (LevelCount == 1) ControllerDialogs.ShowNextDialog(0);
        if (LevelCount == 2) ControllerDialogs.ShowNextDialog(7);
        if (LevelCount == 3) ControllerDialogs.ShowNextDialog(11);
        if (LevelCount == 4) ControllerDialogs.ShowNextDialog(16);
        if (LevelCount == 5) ControllerDialogs.ShowNextDialog(21);
        if (LevelCount == 6) ControllerDialogs.ShowNextDialog(26);
        if (LevelCount == 7) ControllerDialogs.ShowNextDialog(31);
        if (LevelCount == 8) ControllerDialogs.ShowNextDialog(35);
        if (LevelCount == 9) ControllerDialogs.ShowNextDialog(42);
        if (LevelCount == 10) ControllerDialogs.ShowNextDialog(46);
        if (LevelCount == 11) ControllerDialogs.ShowNextDialog(48);
        if (LevelCount == 12) ControllerDialogs.ShowNextDialog(52);
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

        for (int i = 0; i < LevelIcons.Count; i++) 
        {
            if (i < PlayerPrefs.GetInt("levelCountOn", 1))
                LevelIcons[i].image.sprite = LevelIconsPass;
        }
    }
    public void MainMenu() 
    {
        ControllerMenus[0].OpenMenu();
        ControllerMenus[1].CloseMenu();
        ControllerMenus[2].CloseMenu();
        ControllerMenus[5].CloseMenu();
        ControllerMenus[6].CloseMenu();
    }

    void PlayerReset(int pos = 1) 
    {

        int pickShip = 0;

        int upgeade1 = PlayerPrefs.GetInt("playerHpLevel", 0);
        int upgrade2 = PlayerPrefs.GetInt("playerDpLevel", 0);
        int upgrade3 = PlayerPrefs.GetInt("rocketLevel", 0);
        int upgrade4 = PlayerPrefs.GetInt("playerSpeedLevel", 0);

        bool ship1 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 5 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 0;
        bool ship2 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 10 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 5;
        bool ship3 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 20 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 10;
        bool ship4 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 30 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 20;
        bool ship5 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 30;

        if (ship1) pickShip = 0;
        if (ship2) pickShip = 1;
        if (ship3) pickShip = 2;
        if (ship4) pickShip = 3;
        if (ship5) pickShip = 4;

        if (PlayerHP <= 0 || pickShipActive != pickShip) 
        {
            if (pickShipActive != pickShip && Player != null) Destroy(Player.gameObject);
            pickShipActive = pickShip;

            SpawnPlayers.SpawnLevel(pickShip, pos);

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
            LevelCount++;
            if (PlayerPrefs.GetInt("levelCountOn", 1) < LevelCount)
                PlayerPrefs.SetInt("levelCountOn", LevelCount);
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
        if (BossClone != null) 
        {
            if (BossClone.GetComponent<ColliderBoss>() != null) 
            {
                BossHPBar.value = (float)BossClone.GetComponent<ColliderBoss>().HealthPoints() / BossHPStart;
            }
        }
        else BossHp = 0;
    }
    void EndGame() 
    {
        SpawnMeteors.SpawnRemover();
        SpawnBombs.SpawnRemover();
        SpawnEnemies.SpawnRemover();
        SpawnBarriers.SpawnRemover();
    }

    void BossSpawn(int level) 
    {
        if (BossClone == null) 
        {
            BossClone = Instantiate(Bosses[level].gameObject, transform.position, transform.rotation) as GameObject;
            BossHPStart = BossClone.GetComponent<ColliderBoss>().HealthPoints();
            ControllerMenus[7].OpenMenu();
        }
    }
    void CheckBossTarget() 
    {
        if (BossClone != null && Player != null)
        {
            Player.SetTarget(BossClone.transform);
            BossHp = BossClone.GetComponent<ColliderBoss>().HealthPoints();
            BossClone.GetComponent<ControllerBoss>().SetTarget(Player.gameObject.transform);
            BossHPBar.transform.position = Camera.main.WorldToScreenPoint(BossClone.transform.position);
            //SpawnMeteors.FaceingMeteor(clone.GetComponent<ControllerBoss>().PickSpot());
            //SpawnEnemies.FaceingEnemy(clone.GetComponent<ControllerBoss>().PickSpot());
            //clone.GetComponent<SpriteRenderer>().color = Color.black;
            //if(clone.GetComponent<SpriteRenderer>().color == Color.black)
            //Player.SetTarget(null);
            //else if(clone.GetComponent<SpriteRenderer>().color == Color.white)
            //Player.SetTarget(clone.transform);
            //clone.GetComponent<SpriteRenderer>().color = Color.white;
        }
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

            PlayerReset(3);

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

            PlayerReset(3);

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

            PlayerReset(3);

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

            SpawnBarriers.SpawnLevel(1, 3);
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

            PlayerReset(2);

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

            SpawnEnemies.SpawnRate(0.75f);
        }
        SpawnMeteors.SpawnLevel(3);
        SpawnEnemies.SpawnLevel(3, 2);
        SpawnMeteors.FaceingMeteor(4);
        SpawnEnemies.FaceingEnemy(0);
        Player.AngleControll(90);
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 125 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 100 && !LevelSpawnEnemiesDone))
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
    IEnumerator PlayerLookHold()
    {
        Player.AngleControll(SpawnEnemies.FaceingEnemy());
        yield return new WaitForSeconds(1.0f);
        LookingAtHold = false;
    }
    public void Start9()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 9;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level9()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 9 start");

            PlayerReset(2);

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

            SpawnEnemies.SpawnRate(0.4f);
        }
        SpawnMeteors.SpawnLevel(3, 2);
        SpawnEnemies.SpawnLevel(4, 2);
        SpawnMeteors.FaceingMeteor(0);
        SpawnEnemies.FaceingEnemy(Random.Range(0, 3)); 
        if (!LookingAtHold)
        {
            StartCoroutine(PlayerLookHold());
            LookingAtHold = true;
        }
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
            Debug.Log("Level 9 Done");
        }
    }
    public void Start10()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 10;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level10()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 10 start");

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
        SpawnMeteors.SpawnLevel(4, 2);
        SpawnEnemies.SpawnLevel(5, 3);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(Random.Range(2, 5));
        if (!LookingAtHold)
        {
            StartCoroutine(PlayerLookHold());
            LookingAtHold = true;
        }
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 175 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 150 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnMeteors.MeteorDone() && SpawnEnemies.EnemyDone() && LevelSpawnMeteorsDone && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
            Debug.Log("Level 10 Done");
        }
    }
    public void Start11()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 11;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level11()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 11 start");

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
        SpawnMeteors.SpawnLevel(4, 2);
        SpawnEnemies.SpawnLevel(5, 3);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(Random.Range(0, 5));
        if (!LookingAtHold)
        {
            StartCoroutine(PlayerLookHold());
            LookingAtHold = true;
        }
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
            Debug.Log("Level 11 Done");
        }
    }
    public void Start12()
    {
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 12;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level12()
    {
        if (!LevelSetUpDone)
        {
            Debug.Log("Level 12 start");

            PlayerReset();

            Player.SetTarget(null);
            
            BossSpawn(0);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;
            LevelBossDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(0.75f);

        }

        CheckBossTarget();

        SpawnEnemies.SpawnLevel(6, 4);
        if (BossClone != null) 
        {
            SpawnEnemies.FaceingEnemy(BossClone.GetComponent<ControllerBoss>().PickSpot());
        }
        if (PlayerHP <= 0 || BossHp <= 0 || (SpawnEnemies.SpawnCounter() >= 100 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (BossHp <= 0 || PlayerHP <= 0) 
        {
            LevelBossDone = true;
            SpawnEnemies.SpawnRemover();
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone && LevelBossDone)
        {
            Destroy(BossClone);
            ControllerMenus[7].CloseMenu();
            MenuSetUp();
            Debug.Log("Level 12  Done");
        }
    }
}
