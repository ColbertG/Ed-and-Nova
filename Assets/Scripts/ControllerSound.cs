using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> BackGroundClips;
    [SerializeField]
    AudioSource BackGroundAudioSource;
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
        BackGroundAudioSource.loop = true;
        BackGroundAudioSource.Play();
    }
}
