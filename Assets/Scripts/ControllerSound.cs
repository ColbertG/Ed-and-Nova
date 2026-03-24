using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ControllerSound : MonoBehaviour
{
    public static ControllerSound Instance;

    [SerializeField]
    AudioSource BackGroundAudioSource;
    [SerializeField]
    List<AudioClip> BackGroundClips;
    [SerializeField]
    AudioSource RandomGameAudioSource;
    [SerializeField]
    List<AudioClip> RandomGameClips;

    bool gameOverDone = false;
    bool FireNowEnemy = false;
    bool FireNow = false;
    bool BombNow = false;
    bool RocketExplosionNow = false;
    bool MeteorExplosionNow = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOverDone) 
        {
            if (!RandomGameAudioSource.isPlaying) 
            {
                gameOverDone = false;
                RandomGameAudioSource.clip = null;
                BackGroundAudioSource.Play();
            }
        }
    }

    IEnumerator EnemyFire()
    {
        FireNowEnemy = true;
        yield return new WaitForSeconds(1.0f);
        FireNowEnemy = false;
    }
    IEnumerator PlayerFire()
    {
        FireNow = true;
        yield return new WaitForSeconds(1.0f);
        FireNow = false;
    }
    IEnumerator BombFire()
    {
        BombNow = true;
        yield return new WaitForSeconds(1.0f);
        BombNow = false;
    }
    IEnumerator RocketExplosionFire()
    {
        RocketExplosionNow = true;
        yield return new WaitForSeconds(1.0f);
        RocketExplosionNow = false;
    }
    IEnumerator MeteorExplosionFire()
    {
        MeteorExplosionNow = true;
        yield return new WaitForSeconds(1.0f);
        MeteorExplosionNow = false;
    }

    public void BackGroundSound(int pick) 
    {
        if (pick == 1) BackGroundAudioSource.clip = BackGroundClips[0];
        if (pick == 2) BackGroundAudioSource.clip = BackGroundClips[1];
        if (pick == 3) BackGroundAudioSource.clip = BackGroundClips[2];
        if (BackGroundAudioSource != null && RandomGameClips[pick - 1] != null) 
        {
            BackGroundAudioSource.loop = true;
            BackGroundAudioSource.volume = 0.25f;
            BackGroundAudioSource.Play();
        }

    }
    public void RockFire()
    {
        if (RandomGameAudioSource != null && RandomGameClips[0] != null && !FireNow)
        {
            StartCoroutine(PlayerFire());
            RandomGameAudioSource.volume = 1.0f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[0], 0.25f);
        }
    }
    public void RockFireEnemy() 
    {
        if (RandomGameAudioSource != null && RandomGameClips[11] != null && !FireNowEnemy)
        {
            StartCoroutine(EnemyFire());
            RandomGameAudioSource.volume = 1.0f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[11], 0.25f);
        }
    }
    public void MeteorExplosion() 
    {
        if (RandomGameAudioSource != null && RandomGameClips[1] != null && !MeteorExplosionNow)
        {
            StartCoroutine(MeteorExplosionFire());
            RandomGameAudioSource.volume = 0.15f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[1], 0.15f);
        }
    }
    public void GameOver()
    {
        if (RandomGameAudioSource != null && RandomGameClips[2] != null)
        {
            gameOverDone = true;
            BackGroundAudioSource.Stop();
            RandomGameAudioSource.clip = RandomGameClips[2];
            RandomGameAudioSource.volume = 0.25f;
            RandomGameAudioSource.Play();
        }
    }
    public void RocketExplosion()
    {
        if (RandomGameAudioSource != null && RandomGameClips[3] != null && !RocketExplosionNow)
        {
            StartCoroutine(RocketExplosionFire());
            RandomGameAudioSource.volume = 0.25f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[3], 0.25f);
        }
    }
    public void BombExplosion()
    {
        if (RandomGameAudioSource != null && RandomGameClips[4] != null && !BombNow)
        {
            StartCoroutine(BombFire());
            RandomGameAudioSource.volume = 1.0f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[4], 1.0f);
        }
    }
    public void ShipExplosion()
    {
        if (RandomGameAudioSource != null && RandomGameClips[5] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[5], 0.5f);
        }
    }
    public void Button()
    {
        if (RandomGameAudioSource != null && RandomGameClips[6] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[6], 0.5f);
        }
    }
    public void Laser()
    {
        if (RandomGameAudioSource != null && RandomGameClips[7] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[7], 0.5f);
        }
    }
    public void ButtonPowerUps()
    {
        if (RandomGameAudioSource != null && RandomGameClips[8] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[8], 0.5f);
        }
    }
    public void PauseResume()
    {
        if (RandomGameAudioSource != null && RandomGameClips[9] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[9], 0.5f);
        }
    }
    public void Dialog()
    {
        if (RandomGameAudioSource != null && RandomGameClips[10] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[10], 0.5f);
        }
    }
    public void HpPowerUps()
    {
        if (RandomGameAudioSource != null && RandomGameClips[12] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[12], 0.5f);
        }
    }
    public void BarrierPowerUps()
    {
        if (RandomGameAudioSource != null && RandomGameClips[13] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[13], 0.5f);
        }
    }
    public void DestroyPowerUps()
    {
        if (RandomGameAudioSource != null && RandomGameClips[14] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[14], 0.5f);
        }
    }
    public void SlowDownPowerUps()
    {
        if (RandomGameAudioSource != null && RandomGameClips[15] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[15], 0.5f);
        }
    }
    public void PlayerSheild()
    {
        if (RandomGameAudioSource != null && RandomGameClips[16] != null)
        {
            RandomGameAudioSource.volume = 0.5f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[16], 0.5f);
        }
    }

}
