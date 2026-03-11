using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        if (RandomGameAudioSource != null && RandomGameClips[0] != null) 
        {
            RandomGameAudioSource.volume = 0.15f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[0], 0.15f);
        }
    }
    public void MeteorExplosion() 
    {
        if (RandomGameAudioSource != null && RandomGameClips[1] != null)
        {
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
        if (RandomGameAudioSource != null && RandomGameClips[3] != null)
        {
            RandomGameAudioSource.volume = 0.25f;
            RandomGameAudioSource.PlayOneShot(RandomGameClips[3], 0.25f);
        }
    }
    public void BombExplosion()
    {
        if (RandomGameAudioSource != null && RandomGameClips[4] != null)
        {
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
}
