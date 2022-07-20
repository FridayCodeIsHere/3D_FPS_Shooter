using UnityEngine;

public enum TypeOfTarget
{
    HorizontalMovement,
    VerticalMovement,
    RadiusMovement
}
[RequireComponent(typeof(Rigidbody))]
public class TargetComponent : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _finishPosition;
    private Vector3 _startPosition;
    private bool _isMovingForward = true;

    [SerializeField] private TypeOfTarget _typeTarget;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _radiusRotation;
    [SerializeField] private float _distanceX;
    [SerializeField] private float _distanceY;

    public TypeOfTarget TypeTarget => _typeTarget;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_movementSpeed < 0f) _movementSpeed = 0f;
        if (_distanceX < 0f) _distanceX = 0;
        if (_distanceY < 0f) _distanceY = 0;
    }
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        switch(_typeTarget)
        {
            case TypeOfTarget.HorizontalMovement:
                _finishPosition = _startPosition + transform.right * _distanceX;
                break;
            case TypeOfTarget.VerticalMovement:
                _finishPosition = _startPosition + transform.up * _distanceY;
                break;
        }
    }

    private void FixedUpdate()
    {
        switch(_typeTarget)
        {
            case TypeOfTarget.HorizontalMovement:
                HorizontalMovement();
                break;
            case TypeOfTarget.VerticalMovement:
                VerticalMovement();
                break;
        }
    }

    private void CheckBoundaries()
    {
        bool isOutEndBoundary = _isMovingForward && Vector3.Distance(transform.position, _finishPosition) < 0.1f;
        bool isOutStartBoundary = !_isMovingForward && Vector3.Distance(transform.position, _startPosition) < 0.1f;

        if (isOutEndBoundary || isOutStartBoundary)
        {
            _isMovingForward = !_isMovingForward;
        }
    }

    private void HorizontalMovement()
    {
        CheckBoundaries();
        Vector3 nextPoint = transform.right * _movementSpeed * Time.fixedDeltaTime;
        if (!_isMovingForward)
        {
            nextPoint = transform.right * -1 * _movementSpeed * Time.fixedDeltaTime;
        }
        _rb.MovePosition(transform.position + nextPoint);
    }

    private void VerticalMovement()
    {
        CheckBoundaries();
        Vector3 nextPoint = transform.up * _movementSpeed * Time.fixedDeltaTime;
        if (!_isMovingForward)
        {
            nextPoint = transform.up * -1 * _movementSpeed * Time.fixedDeltaTime;
        }
        _rb.MovePosition(transform.position + nextPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        switch (_typeTarget)
        {
            case TypeOfTarget.HorizontalMovement:
                Vector3 finishPointX = transform.position + transform.right * _distanceX;
                Gizmos.DrawLine(transform.position, finishPointX);
                break;
            case TypeOfTarget.VerticalMovement:
                Vector3 finishPointY = transform.position + transform.up * _distanceY;
                Gizmos.DrawLine(transform.position, finishPointY);
                break;
            case TypeOfTarget.RadiusMovement:
                break;
        }
    }
}
