using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private float _fireRate;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private bool _canAutoFire;
    [SerializeField] private int _countAmmo;
    [SerializeField] private GUIAmmo _guiAmmo;

    private float _fireCounter;
    public bool CanFire { get; private set; }
    public bool CanAutoFire => _canAutoFire;
    public int CountAmmo => _countAmmo;
    public float ReloadTime => _fireRate;
    public event WeaponAction OnShoot;
    public event WeaponAction OnApplyAmmo;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_fireRate < 0) _fireRate = 0;
        if (_countAmmo < 0) _countAmmo = 0;
    }
    #endregion

    private void OnEnable()
    {
        OnShoot += _guiAmmo.RenderAmmoData;
        OnApplyAmmo += _guiAmmo.RenderAmmoData;
        OnShoot?.Invoke(_countAmmo);
    }

    private void OnDisable()
    {
        OnShoot -= _guiAmmo.RenderAmmoData;
        OnApplyAmmo -= _guiAmmo.RenderAmmoData;
    }

    private void Update()
    {
        if (!CanFire)
        {
            _fireCounter -= Time.deltaTime;
            if (_fireCounter <= 0)
            {
                CanFire = true;
            }
        }
    }

    public void Shoot()
    {
        if (CanFire && CountAmmo > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out hit, 70f))
            {
                _firePoint.LookAt(hit.point);
            }
            else
            {
                _firePoint.LookAt(_cameraTransform.position + (_cameraTransform.forward * 70f));

            }
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);

            
            _countAmmo--;
            CanFire = false;
            _fireCounter = _fireRate;
            OnShoot?.Invoke(_countAmmo);
        }
        
    }

    public void ApplyAmmo(int count)
    {
        _countAmmo += count;
        OnApplyAmmo?.Invoke(_countAmmo);
    }
}
