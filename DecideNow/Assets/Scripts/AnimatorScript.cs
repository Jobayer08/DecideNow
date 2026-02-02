using UnityEngine;

public class ScenarioAnimatorController : MonoBehaviour
{
    public Animator animator;

    public void PlayIdle()
    {
        animator.SetTrigger("IdleTrigger");
    }

    public void PlayFire()
    {
        animator.SetTrigger("FireTrigger");
    }

    public void PlayAccident()
    {
        animator.SetTrigger("AccidentTrigger");
    }

    public void PlaySurgery()
    {
        animator.SetTrigger("SurgeryTrigger");
    }

    public void PlayCyberAttack()
    {
        animator.SetTrigger("CyberAttackTrigger");
    }
}
