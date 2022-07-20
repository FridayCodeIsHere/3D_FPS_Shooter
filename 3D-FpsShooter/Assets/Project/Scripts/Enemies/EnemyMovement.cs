using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToLose;
    [SerializeField] private Transform _target;

    private Vector3 _startPoint;
    private NavMeshAgent _navMeshAgent;
    private bool _isChasing;
    private const float MultiplierSpeed = 20f;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_speed < 0) _speed = 0;
        if (_distanceToChase < 0f) _distanceToChase = 0f;
        if (_distanceToLose < _distanceToChase) _distanceToLose = _distanceToChase;
    }
    #endregion

    private void Start()
    {
        _startPoint = transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 targetPoint = _target.position;
        targetPoint.y = transform.position.y;

        if (!_isChasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < _distanceToChase)
            {
                _isChasing = true;
            }
        }
        else
        {
            _navMeshAgent.destination = targetPoint;
            if (Vector3.Distance(transform.position, targetPoint) > _distanceToLose)
            {
                _isChasing = false;
            }
        }
        
    }
}
