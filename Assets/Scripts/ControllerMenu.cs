using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField]
    Animator Animators;
    AnimatorStateInfo AnimStateInfo;
    void Update()
    {
        AnimStateInfo = Animators.GetCurrentAnimatorStateInfo(0);
    }
    public void OpenMenu() 
    {
        Animators.SetBool("Close", false);
    }
    public void CloseMenu() 
    {
        Animators.SetBool("Close", true);
    }
}
