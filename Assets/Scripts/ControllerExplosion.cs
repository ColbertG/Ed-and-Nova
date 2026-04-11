using UnityEngine;

public class ControllerExplosion : MonoBehaviour
{
    [SerializeField]
    Animator Animators;
    [SerializeField]
    string AnimationsName = "Explosion1";
    AnimatorStateInfo AnimStateInfo;
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
