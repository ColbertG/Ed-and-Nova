using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField]
    Animator Animators;
    AnimatorStateInfo AnimStateInfo;
    private void Start()
    {
        //gameObject.SetActive(false);
    }
    void Update()
    {
        AnimStateInfo = Animators.GetCurrentAnimatorStateInfo(0);
        //if (AnimStateInfo.normalizedTime >= 1.0f && AnimStateInfo.IsName("CloseWindow"))
            //gameObject.SetActive(false);
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
