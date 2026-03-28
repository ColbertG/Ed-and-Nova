using System.Collections.Generic;
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
    [SerializeField]
    Sprite LevelIconsOn;
    [SerializeField]
    ControllerBackGroundPic BackGroundPics;
    [SerializeField]
    GameObject ShipLevelingUp;
    [SerializeField]
    ControllerSound Sound;

    bool StartGameNow = false;
    bool DialogDone = false;
    bool LevelComplete = false;
    bool LevelSetUpDone = false;
    bool LevelSpawnMeteorsDone = false;
    bool LevelSpawnEnemiesDone = false;
    bool LevelSpawnBombsDone = false;
    bool LevelBossDone = false;
    bool FAB = false;

    bool GamePointsDone = false;
    bool GameOverSound = false;

    int ShowScore = 0;
    int ShowKills = 0;
    int ShowHighScore = 0;
    int ShowCrystal = 0;

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

    int CountLevelSpawn = 0;

    float playerHighScoreMul = 1.0f;

    private void OnApplicationQuit()
    {
    //    PlayerPrefs.SetInt("playerHp", 0);
    //    PlayerPrefs.SetInt("playerDp", 0);
    //    PlayerPrefs.SetInt("rocketHp", 0);
    //    PlayerPrefs.SetInt("rocketDp", 0);
    //    PlayerPrefs.SetInt("playerSpeed", 0);

    //    PlayerPrefs.SetInt("playerHpLevel", 0);
    //    PlayerPrefs.SetInt("playerDpLevel", 0);
    //    PlayerPrefs.SetInt("rocketLevel", 0);
    //    PlayerPrefs.SetInt("playerSpeedLevel", 0);

    //    PlayerPrefs.SetInt("playerRp", 0);

    //    PlayerPrefs.SetInt("scoreKeeper", 0);

    //    PlayerPrefs.SetInt("levelCountOn", 1);

    //    PlayerPrefs.SetInt("playerKills", 0);

    //    PlayerPrefs.SetInt("highScore", 0);

    //    PlayerPrefs.SetInt("Level2Up", 0);
    //    PlayerPrefs.SetInt("Level3Up", 0);
    //    PlayerPrefs.SetInt("Level4Up", 0);
    //    PlayerPrefs.SetInt("Level5Up", 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        Sound.BackGroundSound(1);
        BackGroundPics.SetBackGround(0);
        BackGroundPics.MoveToTarget(1, 2);
        ControllerMenus[0].OpenMenu();


        PointCount[12].text = "HighScore::" + (PlayerPrefs.GetInt("highScore", 0) * PlayerPrefs.GetFloat("playerHighScoreMul", 1)).ToString("00000000000000000");

    }
    // Update is called once per frame
    void Update()
    {
        PointCount[0].text = PlayerPrefs.GetInt("scoreKeeper", 0).ToString("00000000000");
        PointCount[1].text = PlayerPrefs.GetInt("playerRp", 0).ToString("00000000000");

        PointCount[6].text = PlayerHP + " / " + PlayerHPStart;
        PointCount[7].text = PlayerRP + " / " + PlayerRPStart;
        PointCount[8].text = BossHp + " / " + BossHPStart;

        PointCount[14].text = "Level :: " + LevelCount;

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
                if (LevelCount == 13 && !LevelComplete) Level13();
                if (LevelCount == 14 && !LevelComplete) Level14();
                if (LevelCount == 15 && !LevelComplete) Level15();
                if (LevelCount == 16 && !LevelComplete) Level16();
                if (LevelCount == 17 && !LevelComplete) Level17();
                if (LevelCount == 18 && !LevelComplete) Level18();
                if (LevelCount == 19 && !LevelComplete) Level19();
                if (LevelCount == 20 && !LevelComplete) Level20();
                if (LevelCount == 21 && !LevelComplete) Level21();
                if (LevelCount == 22 && !LevelComplete) Level22();
                if (LevelCount == 23 && !LevelComplete) Level23();
                if (LevelCount == 24 && !LevelComplete) Level24();
                if (LevelCount == 25 && !LevelComplete) Level25();
                if (LevelCount == 26 && !LevelComplete) Level26();
                if (LevelCount == 27 && !LevelComplete) Level27();
                if (LevelCount == 28 && !LevelComplete) Level28();
                if (LevelCount == 29 && !LevelComplete) Level29();
                if (LevelCount == 30 && !LevelComplete) Level30();
                if (LevelCount == 31 && !LevelComplete) Level31();
                if (LevelCount == 32 && !LevelComplete) Level32();
                CheckHealth();
                if (PlayerHP <= 0) EndGame();
                if (PlayerHP > 0) 
                {
                    SpawnMeteors.CrystalTarget(Player.gameObject.transform);
                    SpawnEnemies.CrystalTarget(Player.gameObject.transform);
                }
                
            }
        }
    }

    public void HpUpgrade() 
    {
        if (PlayerRP < 20000) return;
        ControllerSound.Instance.ButtonPowerUps();
        if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-20000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 20000);
            PlayerRP = PlayerPrefs.GetInt("playerRp");
        }

        int level = PlayerPrefs.GetInt("playerHpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerHpLevel", level);
        PointCount[2].text = PlayerPrefs.GetInt("playerHpLevel", 0).ToString("000");

        int newHP = PlayerPrefs.GetInt("playerHp", 0) + 10;
        PlayerPrefs.SetInt("playerHp", newHP); 
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints(10);
    }
    public void DpUpgrade()
    {
        if (PlayerRP < 10000) return;
        ControllerSound.Instance.ButtonPowerUps();
        if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-10000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else 
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 10000);
            PlayerRP = PlayerPrefs.GetInt("playerRp");
        }

        int level = PlayerPrefs.GetInt("playerDpLevel", 0) + 1;
        PlayerPrefs.SetInt("playerDpLevel", level);
        PointCount[3].text = PlayerPrefs.GetInt("playerDpLevel", 0).ToString("000");

        int newDP = PlayerPrefs.GetInt("playerDp", 0) + 1;
        PlayerPrefs.SetInt("playerDp", newDP);
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ColliderPlayer>().DestructionPoints(1);
    }
    public void RocketUpgrade()
    {
        if (PlayerRP < 15000) return;
        ControllerSound.Instance.ButtonPowerUps();
        if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-15000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 15000);
            PlayerRP = PlayerPrefs.GetInt("playerRp");
        }

        int level = PlayerPrefs.GetInt("rocketLevel", 0) + 1;
        PlayerPrefs.SetInt("rocketLevel", level);
        PointCount[4].text = PlayerPrefs.GetInt("rocketLevel", 0).ToString("000");

        int newHP = PlayerPrefs.GetInt("rocketHp", 0) + 1;
        int newDP = PlayerPrefs.GetInt("rocketDp", 0) + 1;
        PlayerPrefs.SetInt("rocketHp", newHP);
        PlayerPrefs.SetInt("rocketDp", newDP);
    }
    public void SpeedUpgrade()
    {
        if (PlayerRP < 5000) return;
        ControllerSound.Instance.ButtonPowerUps();
        if (PlayerHP > 0)
        {
            PlayerRP = Player.gameObject.GetComponent<ColliderPlayer>().RewardPoints(-5000);
            PlayerPrefs.SetInt("playerRp", PlayerRP);
        }
        else
        {
            PlayerPrefs.SetInt("playerRp", PlayerPrefs.GetInt("playerRp") - 5000);
            PlayerRP = PlayerPrefs.GetInt("playerRp");
        }

        int level = PlayerPrefs.GetInt("playerSpeedLevel", 0) + 1;
        PlayerPrefs.SetInt("playerSpeedLevel", level);
        PointCount[5].text = PlayerPrefs.GetInt("playerSpeedLevel", 0).ToString("000");

        int newSpeed = PlayerPrefs.GetInt("playerSpeed", 0) + 1;
        PlayerPrefs.SetInt("playerSpeed", newSpeed);
        if (PlayerHP > 0)
            Player.gameObject.GetComponent<ControllerPlayer>().SetSpeed(1);
    }

    public void UpgradeMenu()
    {
        ControllerSound.Instance.Button();
        PointCount[2].text = PlayerPrefs.GetInt("playerHpLevel", 0).ToString("000");
        PointCount[3].text = PlayerPrefs.GetInt("playerDpLevel", 0).ToString("000");
        PointCount[4].text = PlayerPrefs.GetInt("rocketLevel", 0).ToString("000");
        PointCount[5].text = PlayerPrefs.GetInt("playerSpeedLevel", 0).ToString("000");

        ControllerMenus[1].CloseMenu();

        ControllerMenus[2].CloseMenu();

        ControllerMenus[5].OpenMenu();
    }

    public void PauseResumeGame() 
    {

        ControllerSound.Instance.PauseResume();
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
        if(onCount == 157)
            ControllerDialogs.ShowNextDialog(onCount--);
    }
    public void NextDialog()
    {
        ControllerSound.Instance.Dialog();
        int onCount = ControllerDialogs.ShowNextDialog();
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
        DialogEnd(onCount, 59);
        DialogEnd(onCount, 63);
        DialogEnd(onCount, 66);
        DialogEnd(onCount, 70);
        DialogEnd(onCount, 74);
        DialogEnd(onCount, 78);
        DialogEnd(onCount, 85);
        DialogEnd(onCount, 93);
        DialogEnd(onCount, 97);
        DialogEnd(onCount, 103);
        DialogEnd(onCount, 105);
        DialogEnd(onCount, 110);
        DialogEnd(onCount, 129);
        DialogEnd(onCount, 133);
        DialogEnd(onCount, 136);
        DialogEnd(onCount, 140);
        DialogEnd(onCount, 144);
        DialogEnd(onCount, 146);
        DialogEnd(onCount, 148);
        DialogEnd(onCount, 157);
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
        if (LevelCount == 13) ControllerDialogs.ShowNextDialog(56);
        if (LevelCount == 14) ControllerDialogs.ShowNextDialog(60);
        if (LevelCount == 15) ControllerDialogs.ShowNextDialog(64);
        if (LevelCount == 16) ControllerDialogs.ShowNextDialog(67);
        if (LevelCount == 17) ControllerDialogs.ShowNextDialog(71);
        if (LevelCount == 18) ControllerDialogs.ShowNextDialog(75);
        if (LevelCount == 19) ControllerDialogs.ShowNextDialog(79);
        if (LevelCount == 20) ControllerDialogs.ShowNextDialog(86);
        if (LevelCount == 21) ControllerDialogs.ShowNextDialog(94);
        if (LevelCount == 22) ControllerDialogs.ShowNextDialog(98);
        if (LevelCount == 23) ControllerDialogs.ShowNextDialog(104);
        if (LevelCount == 24) ControllerDialogs.ShowNextDialog(106);
        if (LevelCount == 25) ControllerDialogs.ShowNextDialog(111);
        if (LevelCount == 26) ControllerDialogs.ShowNextDialog(130);
        if (LevelCount == 27) ControllerDialogs.ShowNextDialog(134);
        if (LevelCount == 28) ControllerDialogs.ShowNextDialog(137);
        if (LevelCount == 29) ControllerDialogs.ShowNextDialog(141);
        if (LevelCount == 30) ControllerDialogs.ShowNextDialog(145);
        if (LevelCount == 31) ControllerDialogs.ShowNextDialog(147);
        if (LevelCount == 32) ControllerDialogs.ShowNextDialog(149);
        ControllerDialogs.ShowDialog();
    }

    public void StartGame()
    {
        ControllerSound.Instance.Button();
        Sound.BackGroundSound(1);
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[0].CloseMenu();

        LevelCount = 1;
        LevelSetUpDone = false;
        LevelComplete = false;
        StartGameNow = true;

        DialogDone = false;

        ControllerMenus[3].OpenMenu();
        DialogReset();

        if(PlayerHP <= 0 )
            PlayerPrefs.SetInt("scoreKeeper", 0);
    }
    public void NextLevel()
    {
        ControllerSound.Instance.Button();
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
        ControllerSound.Instance.Button();
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
    }
    public void PickLevel()
    {
        ControllerSound.Instance.Button();
        ControllerMenus[0].CloseMenu();

        ControllerMenus[6].OpenMenu();

        for (int i = 0; i <= LevelIcons.Count - 1; i++) 
        {
            if (i < PlayerPrefs.GetInt("levelCountOn", 1) - 1)
                LevelIcons[i].image.sprite = LevelIconsPass;
            if (i == PlayerPrefs.GetInt("levelCountOn", 1) - 1)
                LevelIcons[i].image.sprite = LevelIconsOn;
        }
    }
    public void MainMenu()
    {

        PointCount[12].text = "HighScore::" + (PlayerPrefs.GetInt("highScore", 0) * PlayerPrefs.GetFloat("playerHighScoreMul", 1)).ToString("00000000000000000");

        ControllerSound.Instance.Button();
        Sound.BackGroundSound(1);
        BackGroundPics.SetBackGround(0);
        BackGroundPics.MoveToTarget(1, 2);
        if (Player != null) Player.AngleControll(-90);
        ControllerMenus[0].OpenMenu();
        ControllerMenus[1].CloseMenu();
        ControllerMenus[2].CloseMenu();
        ControllerMenus[5].CloseMenu();
        ControllerMenus[6].CloseMenu();
        ControllerMenus[8].CloseMenu();

        SpawnMeteors.SpawnCounter(true);
        SpawnEnemies.SpawnCounter(true);
        SpawnBarriers.SpawnRemover();
    }
    public void GameInfoMenu()
    {
        ControllerSound.Instance.Button();
        ControllerMenus[0].CloseMenu();
        ControllerMenus[8].OpenMenu();
    }

    void PlayerReset(int pos = 1) 
    {
        int pickShip = 0;

        int upgeade1 = PlayerPrefs.GetInt("playerHpLevel", 0);
        int upgrade2 = PlayerPrefs.GetInt("playerDpLevel", 0);
        int upgrade3 = PlayerPrefs.GetInt("rocketLevel", 0);
        int upgrade4 = PlayerPrefs.GetInt("playerSpeedLevel", 0);

        bool ship1 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 5 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 0;
        bool ship2 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 30 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 5;
        bool ship3 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 80 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 30;
        bool ship4 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) < 180 && (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 80;
        bool ship5 = (upgeade1 + upgrade2 + upgrade3 + upgrade4) >= 180;

        if (ship1) pickShip = 0;
        if (ship2) pickShip = 1; 
        if (ship3) pickShip = 2;
        if (ship4) pickShip = 3;
        if (ship5) pickShip = 4;
        if(LevelCount == 23 || LevelCount == 24) pickShip = 5;
        
        if (PlayerHP <= 0 || pickShipActive != pickShip) 
        {
            if (PlayerHP <= 0) 
            {
                PlayerPrefs.SetInt("scoreKeeper", 0);
                PlayerPrefs.SetInt("playerKills", 0);

                ShowScore = 0;
                ShowKills = 0;
                ShowHighScore = 0;
                ShowCrystal = 0;

                GamePointsDone = false;
                GameOverSound = false;
            }
            if (pickShipActive != pickShip && Player != null) Destroy(Player.gameObject);
            pickShipActive = pickShip;

            SpawnPlayers.SpawnLevel(pickShip, pos);

            Player = SpawnPlayers.ActivePlayer().GetComponent<ControllerPlayer>();

            if (PlayerPrefs.GetInt("Level2Up", 0) == 0 && pickShip == 1)
            {
                PlayerPrefs.SetInt("Level2Up", 1);
                GameObject x = Instantiate(ShipLevelingUp, Player.transform.position, Player.transform.rotation) as GameObject;
            }
            if (PlayerPrefs.GetInt("Level3Up", 0) == 0 && pickShip == 2)
            {
                PlayerPrefs.SetInt("Level3Up", 1);
                GameObject x = Instantiate(ShipLevelingUp, Player.transform.position, Player.transform.rotation) as GameObject;
            }
            if (PlayerPrefs.GetInt("Level4Up", 0) == 0 && pickShip == 3)
            {
                PlayerPrefs.SetInt("Level4Up", 1);
                GameObject x = Instantiate(ShipLevelingUp, Player.transform.position, Player.transform.rotation) as GameObject;
            }
            if (PlayerPrefs.GetInt("Level5Up", 0) == 0 && pickShip == 4)
            {
                PlayerPrefs.SetInt("Level5Up", 1);
                GameObject x = Instantiate(ShipLevelingUp, Player.transform.position, Player.transform.rotation) as GameObject;
            }

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
            if(LevelCount < 31) LevelCount++;
            else LevelCount = 1;
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
    void CheckHealth()
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
        playerHighScoreMul = 1.0f;
        int score = PlayerPrefs.GetInt("scoreKeeper", 0);
        int kills = PlayerPrefs.GetInt("playerKills", 0);
        int highScore = Mathf.Clamp((PlayerPrefs.GetInt("scoreKeeper", 0) * PlayerPrefs.GetInt("playerKills", 0)), 0, int.MaxValue);
        if (((PlayerPrefs.GetInt("scoreKeeper", 0) * PlayerPrefs.GetInt("playerKills", 0))/int.MaxValue) > 1.0f) 
        {
            playerHighScoreMul = ((PlayerPrefs.GetInt("scoreKeeper", 0) * PlayerPrefs.GetInt("playerKills", 0)) / int.MaxValue);
            PlayerPrefs.SetFloat("playerHighScoreMul", playerHighScoreMul);
        }
        int crystalWon = highScore / 10000;

        if (!GameOverSound) 
        {
            GameOverSound = true;
            ControllerSound.Instance.GameOver();

            PlayerPrefs.SetInt("playerRp", (PlayerPrefs.GetInt("playerRp") + crystalWon));
            PlayerRP = PlayerPrefs.GetInt("playerRp", 0);
            if (PlayerPrefs.GetInt("highScore", 0) < highScore) PlayerPrefs.SetInt("highScore", highScore);
        }

        if (score > ShowScore)
        {
            ShowScore += (score/200) + 1;
        }
        else if (kills > ShowKills)
        {
            ShowScore = score;
            ShowKills += (kills/200) + 1;
        }
        else if (highScore > ShowHighScore)
        {
            ShowKills = kills;
            ShowHighScore += (highScore/200) + 1;
        }
        else if (crystalWon > ShowCrystal)
        {
            ShowHighScore = highScore;
            ShowCrystal += (crystalWon/200) + 1;
        }
        else if (!GamePointsDone)
        {
            ShowCrystal = crystalWon;
            GamePointsDone = true;
        }

        PointCount[9].text = "Score::" + ShowScore.ToString("00000000000000000000");
        PointCount[10].text = "Kills::" + ShowKills.ToString("000000000000000000000");
        PointCount[11].text = (ShowHighScore * playerHighScoreMul).ToString("00000000000000000000000000");
        PointCount[13].text = "Crystal::" + ShowCrystal.ToString("00000000000000000");
        
        
        SpawnMeteors.SpawnRemover();
        SpawnBombs.SpawnRemover();
        SpawnEnemies.SpawnRemover();
        SpawnBarriers.SpawnRemover();
    }

    void BossSpawn(int level) 
    {
        if (BossClone == null) 
        {
            BossClone = Instantiate(Bosses[level].gameObject, transform.position + new Vector3(0, 0, 1), transform.rotation) as GameObject;
            BossHPStart = BossClone.GetComponent<ColliderBoss>().HealthPoints();
            BossClone.GetComponent<SpriteRenderer>().color = Color.white;
            ControllerMenus[7].OpenMenu();
        }
    }
    void CheckBossTarget() 
    {
        if (BossClone != null && Player != null)
        {
            if (BossClone.GetComponent<SpriteRenderer>().color == Color.black)
                Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
            else if (BossClone.GetComponent<SpriteRenderer>().color == Color.white)
                Player.SetTarget(BossClone.transform);
            BossHp = BossClone.GetComponent<ColliderBoss>().HealthPoints();
            BossClone.GetComponent<ControllerBoss>().SetTarget(Player.gameObject.transform);
            BossHPBar.transform.position = Camera.main.WorldToScreenPoint(BossClone.transform.position);
        }
    }

    public void Start1()
    {
        ControllerSound.Instance.Button();
        Sound.BackGroundSound(1);
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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
            Sound.BackGroundSound(1);
            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 0);

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
        }
    }
    public void Start2()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 2) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 0);

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
        }
    }
    public void Start3()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 3) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 0);

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
        }
    }
    public void Start4()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 4) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 2);

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
        }
    }
    public void Start5()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 5) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 1);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 1);

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
        }
    }
    public void Start6()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 6) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 1);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 1);

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
        }
    }
    public void Start7()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 7) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 0);

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
        }
    }
    public void Start8()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 8) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(2, 1);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(2, 1);

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
        }
    }
    public void Start9()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 9) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(2, 1);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(2, 1);

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

        if(Player != null)
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        
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
        }
    }
    public void Start10()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 10) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(2, 1);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(2, 1);

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

        if (Player != null)
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        
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
        }
    }
    public void Start11()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 11) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(2, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(2, 0);

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
        
        if (Player != null)
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        
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
        }
    }
    public void Start12()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 12) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(1);
        BackGroundPics.SetBackGround(1);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
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

            BackGroundPics.SetBackGround(1);
            BackGroundPics.MoveToTarget(1, 0);

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

        if (BossHp > BossHPStart / 3)
            SpawnEnemies.SpawnLevel(6, 4);
        else 
            SpawnEnemies.SpawnLevel(6, 0);

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

        if (BossHp <= BossHPStart / 3 && BossHp > 10)
        {
            SpawnEnemies.SpawnRate(0.5f);
            SpawnEnemies.enabled = true;
        }
        else if (LevelSpawnEnemiesDone)
        {
            SpawnEnemies.enabled = false;
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
        }
    }
    public void Start13()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 13) return;

        ControllerSound.Instance.Button();
        Sound.BackGroundSound(2);
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 13;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level13()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 2);

            PlayerReset(2);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(4, 4);
        SpawnEnemies.SpawnLevel(2);
        SpawnMeteors.FaceingMeteor(0);
        SpawnEnemies.FaceingEnemy(4);
        Player.AngleControll(SpawnMeteors.FaceingMeteor());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 250 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            MenuSetUp();
        }
    }
    public void Start14()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 14) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 14;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level14()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 2);


            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(6, 4);
        SpawnEnemies.SpawnLevel(2);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(4);
        Player.AngleControll(SpawnMeteors.FaceingMeteor());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 300 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            MenuSetUp();
        }
    }
    public void Start15()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 15) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 15;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level15()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 2);

            PlayerReset(1);

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

            SpawnEnemies.SpawnRate(0.6f);
        }
        SpawnMeteors.SpawnLevel(6, 4);
        SpawnEnemies.SpawnLevel(7, 6);
        SpawnMeteors.FaceingMeteor(2);
        SpawnEnemies.FaceingEnemy(2);
        if (Player != null) 
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(0.0f, 0.75f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
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
        }
    }
    public void Start16()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 16) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 16;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level16()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 0);

            CountLevelSpawn = 0;

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
            SpawnEnemies.SpawnRate(0.5f);
        }
        if (!FAB)
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter())
            {
                FAB = true;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(7, 6);
            SpawnEnemies.FaceingEnemy(Random.Range(0, 2));
        }
        else
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter())
            {
                FAB = false;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(8, 7);
            SpawnEnemies.FaceingEnemy(Random.Range(3, 5));
        }
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(0.75f, 1.5f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 200 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start17()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 17) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(2, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 17;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level17()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 2);

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
            SpawnEnemies.SpawnRate(0.5f);
        }
        if (!FAB)
        {
            FAB = true;
            SpawnEnemies.SpawnLevel(8, 7);
            SpawnEnemies.FaceingEnemy(0);
        }
        else
        {
            FAB = false;
            SpawnEnemies.SpawnLevel(9, 8);
            SpawnEnemies.FaceingEnemy(4);
        }
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(1.5f, 2.25f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 200 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start18()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 18) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 18;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level18()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 2);

            CountLevelSpawn = 0;

            PlayerReset(2);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
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
        SpawnEnemies.FaceingEnemy(0);
        if (!FAB)
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter()) 
            {
                FAB = true;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(9, 8); 
            if (Player != null)
                SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(2.25f, 3.00f));
        }
        if (FAB)
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter())
            {
                FAB = false;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(10, 9);
            if (Player != null)
                SpawnEnemies.LookAtPlayer(null, 0.0f);
        }
        if (Player != null)
        {
            Player.AngleControll(SpawnEnemies.FaceingEnemy());
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 200 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start19()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 19) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 19;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level19()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(1, 0);

            CountLevelSpawn = 0;

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
            SpawnEnemies.SpawnRate(0.66f);
        }

        SpawnEnemies.SpawnLevel(10, 9);
        SpawnEnemies.FaceingEnemy(2);
        if (Player != null)
            SpawnEnemies.LookAtPlayer(null, 0.0f);
        if (Player != null)
        {
            Player.AngleControll(0);
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 175 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start20()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 20) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(2, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 20;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level20()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(2, 2);

            CountLevelSpawn = 0;

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
            SpawnEnemies.SpawnRate(0.5f);
        }

        SpawnEnemies.SpawnLevel(11, 10);
        SpawnEnemies.FaceingEnemy(2);
        if (Player != null)
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(0.5f, 1.25f));
        if (Player != null)
        {
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 175 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start21()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 21) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(2, 2);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 21;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level21()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(2, 2);


            CountLevelSpawn = 0;

            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
            SpawnEnemies.SpawnRate(0.66f);
        }
        SpawnEnemies.FaceingEnemy(Random.Range(0, 5));
        if (!FAB)
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter())
            {
                FAB = true;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(11, 10);
        }
        else
        {
            if (CountLevelSpawn < SpawnEnemies.SpawnCounter())
            {
                FAB = false;
                CountLevelSpawn = SpawnEnemies.SpawnCounter();
            }
            SpawnEnemies.SpawnLevel(12, 11);
        }
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(2.25f, 3.00f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 250 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start22()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 22) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(2);
        BackGroundPics.MoveToTarget(2, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 22;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level22()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(2);
            BackGroundPics.MoveToTarget(2, 0);

            CountLevelSpawn = 0;

            PlayerReset();

            Player.SetTarget(null);

            BossSpawn(1);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;
            LevelBossDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.00f);
            SpawnMeteors.SpawnRate(0.6f);

        }

        CheckBossTarget();

        SpawnMeteors.SpawnLevel(6, 4);
        
        SpawnEnemies.SpawnLevel(9, 7);

        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(2.25f, 3.00f));
        }

        if (BossClone != null)
        {
            SpawnEnemies.FaceingEnemy(BossClone.GetComponent<ControllerBoss>().PickSpot());
        }

        if (!FAB)
        {
            if (CountLevelSpawn < SpawnMeteors.SpawnCounter())
            {
                FAB = true;
                CountLevelSpawn = SpawnMeteors.SpawnCounter();
            }
            SpawnMeteors.FaceingMeteor(0);
        }
        else
        {
            if (CountLevelSpawn < SpawnMeteors.SpawnCounter())
            {
                FAB = false;
                CountLevelSpawn = SpawnMeteors.SpawnCounter();
            }
            SpawnMeteors.FaceingMeteor(4);
        }

        if (PlayerHP <= 0 || BossHp <= 0 || (SpawnEnemies.SpawnCounter() >= 100 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }

        if (PlayerHP <= 0 || BossHp <= 0 ||(SpawnMeteors.SpawnCounter() >= 1000 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }

        if (BossHp <= BossHPStart / 2 && BossHp > 1000)
        {
            SpawnEnemies.enabled = true;
        }
        else if (LevelSpawnEnemiesDone)
        {
            SpawnEnemies.enabled = false;
        }

        if (BossHp <= 0 || PlayerHP <= 0)
        {
            LevelBossDone = true;
            SpawnEnemies.SpawnRemover();
            SpawnMeteors.SpawnRemover();
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone && LevelBossDone && SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            Destroy(BossClone);
            ControllerMenus[7].CloseMenu();
            MenuSetUp();
        }
    }
    public void Start23()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 23) return;
        ControllerSound.Instance.Button();
        Sound.BackGroundSound(3);
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(1, 3);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 23;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level23()
    {
        if (!LevelSetUpDone)
        {
            Sound.BackGroundSound(3);
            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(1, 3);

            PlayerReset(2);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(7, 6);
        SpawnMeteors.FaceingMeteor(0);
        Player.AngleControll(SpawnMeteors.FaceingMeteor());
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 250 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            MenuSetUp();
        }
    }
    public void Start24()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 24) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(1, 3);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 24;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level24()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(1, 3);

            PlayerReset(2);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = true;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();
        }
        SpawnMeteors.SpawnLevel(8, 6);
        SpawnMeteors.FaceingMeteor(Random.Range(0, 3));
        Player.AngleControll(-90);
        if (PlayerHP <= 0 || (SpawnMeteors.SpawnCounter() >= 250 && !LevelSpawnMeteorsDone))
        {
            SpawnMeteors.SpawnCounter(true);
            SpawnMeteors.enabled = false;
            LevelSpawnMeteorsDone = true;
        }
        if (SpawnMeteors.MeteorDone() && LevelSpawnMeteorsDone)
        {
            MenuSetUp();
        }
    }
    public void Start25()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 25) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(2, 1.5f);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 25;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level25()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(2, 1.5f);

            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.25f);
        }
        SpawnEnemies.SpawnLevel(13, 12);
        SpawnEnemies.FaceingEnemy(Random.Range(0, 5));
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(3.00f, 3.75f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 150 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start26()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 26) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(2, 1.5f);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 26;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level26()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(2, 1.5f);


            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.05f);
        }
        SpawnEnemies.SpawnLevel(14, 13);
        SpawnEnemies.FaceingEnemy(4);
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(3.75f, 4.5f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 175 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start27()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 27) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(1, 1.5f);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 27;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level27()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(1, 1.5f);

            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(0.95f);
        }
        SpawnEnemies.SpawnLevel(15, 14);
        SpawnEnemies.FaceingEnemy(0);
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(4.5f, 5.25f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 200 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start28()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 28) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(1, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 28;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level28()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(1, 0);

            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.15f);
        }
        SpawnEnemies.SpawnLevel(16, 15);
        SpawnEnemies.FaceingEnemy(Random.Range(1, 4));
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(5.25f, 6.0f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 225 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start29()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 29) return;
        ControllerSound.Instance.Button();
        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 29;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level29()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(1, 1.5f);


            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.3f);
        }
        SpawnEnemies.SpawnLevel(17, 16);
        SpawnEnemies.FaceingEnemy(Random.Range(0, 2));
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(5.25f, 6.0f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 250 && !LevelSpawnEnemiesDone) )
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start30()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 30) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(2, 1.0f);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 30;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level30()
    {
        if (!LevelSetUpDone)
        {

            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(2, 1.0f);

            PlayerReset(1);

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = true;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            SpawnEnemies.SpawnRate(1.3f);
        }
        SpawnEnemies.SpawnLevel(18, 17);
        SpawnEnemies.FaceingEnemy(Random.Range(3, 5));
        if (Player != null)
        {
            SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(5.25f, 6.0f));
            Player.SetTarget(SpawnEnemies.LookAtCloset(Player.transform));
        }
        if (PlayerHP <= 0 || (SpawnEnemies.SpawnCounter() >= 275 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }
        if (SpawnEnemies.EnemyDone() && LevelSpawnEnemiesDone)
        {
            MenuSetUp();
        }
    }
    public void Start31()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 31) return;
        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(2, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 31;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level31()
    {
        if (!LevelSetUpDone)
        {
            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(2, 0);

            PlayerReset();

            Player.SetTarget(null);

            BossSpawn(2);

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

            SpawnEnemies.SpawnRate(2.0f);

        }

        CheckBossTarget();

        if (BossHp > BossHPStart / 3)
        {
            SpawnEnemies.SpawnLevel(15, 12);
            if (Player != null)
            {
                SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(0.0f, 3.0f));
            }
        }
        else 
        {
            SpawnEnemies.SpawnLevel(18, 15);
            if (Player != null)
            {
                SpawnEnemies.LookAtPlayer(Player.transform, Random.Range(0.75f, 3.0f));
            }
        }

        SpawnEnemies.FaceingEnemy(Random.Range(0, 5));
        
        if (PlayerHP <= 0 || BossHp <= 0 || (SpawnEnemies.SpawnCounter() >= 150 && !LevelSpawnEnemiesDone))
        {
            SpawnEnemies.SpawnCounter(true);
            SpawnEnemies.enabled = false;
            LevelSpawnEnemiesDone = true;
        }

        if (BossHp <= BossHPStart / 3 && BossHp > 3333)
        {
            SpawnEnemies.SpawnRate(1.5f);
            SpawnEnemies.enabled = true;
        }
        else if (LevelSpawnEnemiesDone)
        {
            SpawnEnemies.enabled = false;
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
        }
    }
    public void Start32()
    {
        if (PlayerPrefs.GetInt("levelCountOn", 1) < 32) return;

        ControllerSound.Instance.Button();
        BackGroundPics.SetBackGround(3);
        BackGroundPics.MoveToTarget(2, 0);

        ControllerMenus[6].CloseMenu();

        ControllerMenus[3].OpenMenu();
        ControllerDialogs.ShowDialog();

        StartGameNow = true;

        if (PlayerHP <= 0)
            PlayerPrefs.SetInt("scoreKeeper", 0);

        LevelCount = 32;
        LevelSetUpDone = false;
        LevelComplete = false;

        DialogDone = false;

        DialogReset();
    }
    void Level32()
    {
        if (!LevelSetUpDone)
        {
            BackGroundPics.SetBackGround(3);
            BackGroundPics.MoveToTarget(2, 2);


            PlayerReset();

            Player.SetTarget(null);

            LevelSetUpDone = true;

            SpawnMeteors.enabled = false;
            SpawnEnemies.enabled = false;
            SpawnBombs.enabled = false;

            LevelSpawnMeteorsDone = false;
            LevelSpawnEnemiesDone = false;
            LevelSpawnBombsDone = false;
            LevelBossDone = false;

            SpawnMeteors.SpawnCounter(true);
            SpawnEnemies.SpawnCounter(true);

            SpawnBarriers.SpawnRemover();

            MenuSetUp();
        }
    }
}
