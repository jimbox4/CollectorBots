using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(Animator))]
public class BotAnimator
{
    private static int Speed = Animator.StringToHash(nameof(Speed));
    private static int IsRelaxing = Animator.StringToHash(nameof(IsRelaxing));
    private static int HasItem = Animator.StringToHash(nameof(HasItem));
    private static int PickUp = Animator.StringToHash(nameof(PickUp));

    [SerializeField] private Animator _animator;

    public bool IsPickUpState => _animator.GetCurrentAnimatorStateInfo(0).IsName(nameof(PickUp));

    public void SetSpeed(float speed)
    {
        _animator.SetFloat(Speed, speed);
    }

    public void SetIsRelax(bool isRelaxing)
    {
        _animator.SetBool(IsRelaxing, isRelaxing);
    }

    public void ResetPickUpState()
    {
        _animator.ResetTrigger(PickUp);
    }

    public void SetPickUpState()
    {
        _animator.SetTrigger(PickUp);
    }

    public void SetHasItem(bool hasItem)
    {
        _animator.SetBool(HasItem, hasItem);
    }
}
