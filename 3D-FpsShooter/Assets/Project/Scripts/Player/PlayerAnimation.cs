using UnityEngine;

[RequireComponent(typeof(Animator), typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private static readonly int _movement = Animator.StringToHash("moveSpeed");
    private static readonly int _groundCheck = Animator.StringToHash("isGround");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        MovementAnim();
        CheckGroundAnimation();
    }

    private void MovementAnim()
    {
        _animator.SetFloat(_movement, _playerMovement.CurrentSpeed);
    }

    private void CheckGroundAnimation()
    {
        _animator.SetBool(_groundCheck, _playerMovement.GroundDetect.IsGround());
    }

}
