using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GroundDetector _groundDetector;
    
    private CharacterController _characterController;
    private Vector3 _moveInput;
    private const float Gravity = 2f;
    private bool _canDoubleJump = false;

    public GroundDetector GroundDetect => _groundDetector;
    public float CurrentSpeed => _moveInput.magnitude;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_moveSpeed < 0) _moveSpeed = 0;
        if (_jumpForce < 0) _jumpForce = 0;
        if (_runSpeed < _moveSpeed) _runSpeed = _moveSpeed;
    }
    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();

        Jumping();
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_groundDetector.IsGround() || _canDoubleJump)
            {
                _canDoubleJump= !_canDoubleJump;
                _moveInput.y = _jumpForce;
            }
        }
    }

    private void Movement()
    {
        float yStore = _moveInput.y;

        Vector3 vertMove = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 horiMove = transform.right * Input.GetAxisRaw("Horizontal");

        _moveInput = horiMove + vertMove;
        _moveInput.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveInput *= _runSpeed;
        }
        else
        {
            _moveInput *= _moveSpeed;
        }

        _moveInput.y = yStore;

        _moveInput.y += Physics.gravity.y * Gravity * Time.deltaTime;

        _characterController.Move(_moveInput * Time.deltaTime);

        if (_characterController.isGrounded)
        {
            _moveInput.y = Physics.gravity.y * Gravity * Time.deltaTime;
        }
    }
    
}
