using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private GroundDetector _groundDetector;

    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _moveInput;

    private static readonly int _moveAnimation = Animator.StringToHash("moveSpeed");
    private static readonly int _isGroundAnimation = Animator.StringToHash("isGround");
    private const float Gravity = 2f;
    private bool _canDoubleJump = false;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_moveSpeed < 0) _moveSpeed = 0;
        if (_mouseSensitivity < 0) _mouseSensitivity = 0;
        if (_jumpForce < 0) _jumpForce = 0;
        if (_runSpeed < _moveSpeed) _runSpeed = _moveSpeed;
    }
    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();

        Jumping();

        CameraRotation();
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

        _animator.SetFloat(_moveAnimation, _moveInput.magnitude);
        _animator.SetBool(_isGroundAnimation, _groundDetector.IsGround());
    }

    private void CameraRotation()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * _mouseSensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
         _cameraTransform.rotation = Quaternion.Euler(_cameraTransform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
    
}
