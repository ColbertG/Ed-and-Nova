using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerExplosion : MonoBehaviour
{
    [SerializeField]
    Animator Animators;
    [SerializeField]
    string AnimationsName = "Explosion1";
    AnimatorStateInfo AnimStateInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimStateInfo = Animators.GetCurrentAnimatorStateInfo(0);
        if (AnimStateInfo.normalizedTime >= 1.0f && AnimStateInfo.IsName(AnimationsName)) 
        {
            Destroy(gameObject);
        }
    }
}
