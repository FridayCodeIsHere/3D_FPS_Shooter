using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int _shotAnim = Animator.StringToHash("FireShot");
    private static readonly int _moveAnim = Animator.StringToHash("IsMoving");


    public void ShotAnimation()
    {
        _animator.SetTrigger(_shotAnim);
    }

    public void MoveAnimation()
    {
        _animator.SetBool(_moveAnim, true);
    }

    public void StopMoveAnimation()
    {
        _animator.SetBool(_moveAnim, false);
    }
}
