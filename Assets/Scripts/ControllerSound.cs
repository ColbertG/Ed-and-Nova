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
}
