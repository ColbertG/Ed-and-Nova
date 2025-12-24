using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ControllerGame : MonoBehaviour
{
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
    // Start is called before the first frame update
    private void OnApplicationQuit()
    {
        //PlayerPrefs.SetInt();
        
    }
    void Start()
    {
        ControllerMenus[0].OpenMenu();
        //ControllerDialogs.ShowDialog();
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
    public void NextDialog() 
    {
        int onCount = ControllerDialogs.ShowNextDialog();
        if (onCount == 6) 
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
        if(LevelCount == 1) ControllerDialogs.ShowNextDialog(0);
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
        ControllerMenus[2].CloseMenu();
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
        if (PlayerHP <= 0) 
        {
            SpawnPlayers.SpawnLevel(0, 1);
            Player = SpawnPlayers.ActivePlayer().GetComponent<ControllerPlayer>();
            PlayerHP = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
        }
    }
    void MenuSetUp() 
    {
        LevelComplete = true;
        if (PlayerHP > 0)
            ControllerMenus[2].OpenMenu();
        else ControllerMenus[1].OpenMenu();
    }
    void CheckPlayerHealth()
    {
        if (Player != null)
        {
            if (Player.gameObject.GetComponent<ColliderPlayer>() != null)
                PlayerHP = Player.gameObject.GetComponent<ColliderPlayer>().HealthPoints();
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
