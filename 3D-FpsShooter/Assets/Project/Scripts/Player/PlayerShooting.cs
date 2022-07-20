using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _cameraTransform;
    private const float _minDistance = 2f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, 50f))
        {
            
            _firePoint.LookAt(hit.point);
            
        }
        else
        {
            _firePoint.LookAt(_cameraTransform.position + (_cameraTransform.forward * 30f));
        }
        Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
    }
}
