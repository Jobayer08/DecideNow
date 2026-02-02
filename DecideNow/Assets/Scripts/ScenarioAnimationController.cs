using UnityEngine;

public class ScenarioAnimationController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayWithReset(string trigger)
    {
        animator.ResetTrigger("IdleTrigger");
        animator.ResetTrigger("FireTrigger");
        animator.ResetTrigger("AccidentTrigger");
        animator.ResetTrigger("SurgeryTrigger");
        animator.ResetTrigger("CyberAttackTrigger");

        animator.SetTrigger(trigger);
    }
}
