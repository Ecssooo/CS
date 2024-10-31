using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    /// <summary>
    /// Do 'die' animation
    /// </summary>
    public void TriggerDieAnimation()
    {
        _animator.SetTrigger("Die");
    }
    
    /// <summary>
    /// Do 'Attack' animation
    /// </summary>
    /// <param name="attackName">Name of the parameter in animator</param>
    public void TriggerAttackAnimation(string attackName)
    {
        _animator.SetTrigger(attackName);
    }

    /// <summary>
    /// Do 'Hit' animation
    /// </summary>
    public void TriggerGetHitAnimation()
    {
        _animator.SetTrigger("GetHit");
    }
    
    /// <summary>
    /// Do 'MoveFWD' animation
    /// </summary>
    /// <param name="state">Start / End animation</param>
    public void SetBoolWalkAnimation(bool state)
    {
        _animator.SetBool("MoveFWD", state);
    }

    /// <summary>
    /// Do 'RunningFWD' animation
    /// </summary>
    /// <param name="state">Start / End animation</param>
    public void SetBoolRunAnimation(bool state)
    {
        _animator.SetBool("RunningFWD", state);
    }
}
