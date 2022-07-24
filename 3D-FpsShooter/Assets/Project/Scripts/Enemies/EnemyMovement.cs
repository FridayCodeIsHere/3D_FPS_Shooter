using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemyAnimator))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _distanceToChase;
    [SerializeField] private float _distanceToLose;
    [SerializeField] private float _keepChasingTime;
    [SerializeField] private float _distanceToStop;
    [SerializeField] private Transform _target;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _waitBetweenShots = 2f;
    [SerializeField] private float _timeToShoot = 1f;

    private float _fireCount;
    private float _shotWaitCounter;
    private float _shootTimeCounter;

    private Vector3 _startPoint;
    private NavMeshAgent _navMeshAgent;
    private EnemyAnimator _enemyAnimator;
    private bool _isChasing;
    private float _chaseCounter;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_distanceToChase < 0f) _distanceToChase = 0f;
        if (_distanceToLose < _distanceToChase) _distanceToLose = _distanceToChase;
        if (_keepChasingTime < 0f) _keepChasingTime = 0f;
        if (_distanceToStop < 0f) _distanceToStop = 0f;
    }
    #endregion

    private void Start()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _startPoint = transform.position;
        _shootTimeCounter = _timeToShoot;
        _shotWaitCounter = _waitBetweenShots;
    }


    

    private void Update()
    {
        Vector3 targetPoint = _target.position;
        targetPoint.y = transform.position.y;

        if (!_isChasing)
        {
            CheckTargetDistance(targetPoint);

            if (_chaseCounter > 0f)
            {
                _chaseCounter -= Time.deltaTime;
                if (_chaseCounter < 0f)
                {
                    _navMeshAgent.destination = _startPoint;
                }
            }

            if (_navMeshAgent.remainingDistance < 0.25f)
            {
                _enemyAnimator.StopMoveAnimation();
            }
            else
            {
                TryToMoveAnimation(targetPoint);
            }
        }
        else
        {
            if (_target.gameObject.activeInHierarchy)
            {
                CheckDistanceToStop(targetPoint);

                CheckDistanceToLose(targetPoint);

                if (_shotWaitCounter > 0)
                {
                    _shotWaitCounter -= Time.deltaTime;

                    if (_shotWaitCounter <= 0)
                    {
                        _shootTimeCounter = _timeToShoot;
                    }

                    TryToMoveAnimation(targetPoint);
                }
                else
                {
                    _shootTimeCounter -= Time.deltaTime;

                    if (_shootTimeCounter > 0)
                    {
                        _fireCount -= Time.deltaTime;


                        if (_fireCount <= 0)
                        {
                            Attacking();
                        }
                        _navMeshAgent.destination = transform.position;
                    }
                    else
                    {
                        _shotWaitCounter = _waitBetweenShots;
                    }
                    _enemyAnimator.StopMoveAnimation();
                }
            }

        }
        
    }

    private void TryToMoveAnimation(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > _distanceToStop)
        {
            _enemyAnimator.MoveAnimation();
        }
        else
        {
            _enemyAnimator.StopMoveAnimation();
        }
    }

    private void CheckTargetDistance(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) < _distanceToChase)
        {
            _isChasing = true;
            _shootTimeCounter = _timeToShoot;
            _shotWaitCounter = _waitBetweenShots;
        }
    }

    private void CheckDistanceToStop(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) > _distanceToStop)
        {
            _navMeshAgent.destination = targetPos;
        }
        else
        {
            _navMeshAgent.destination = transform.position;
        }
    }

    private void CheckDistanceToLose(Vector3 targetPos)
    {
        if (Vector3.Distance(transform.position, targetPos) > _distanceToLose)
        {
            _isChasing = false;
            _chaseCounter = _keepChasingTime;
        }
    }

    private void Attacking()
    {
        _fireCount = _fireRate;
        _firePoint.LookAt(_target.position + new Vector3(0, 0.2f, 0f));

        Vector3 targetDir = _target.position - transform.position;
        float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        if (Mathf.Abs(angle) < 30f)
        {
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
            _enemyAnimator.ShotAnimation();
        }
        else
        {
            _shotWaitCounter = _waitBetweenShots;
        }
    }
}
