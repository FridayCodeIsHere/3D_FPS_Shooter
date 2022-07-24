using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _currentGun;
    [SerializeField] private Gun[] _weapons;
    public Gun CurrentWeapon { get; private set; }

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_currentGun < 0 || _currentGun > _weapons.Length - 1)
        {
            _currentGun = 0;
        } 
    }
    #endregion

    private void Start()
    {
        CurrentWeapon = _weapons[_currentGun];
        _weapons[_currentGun].gameObject.SetActive(true);
    }

    private void Update()
    {
        Attack();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
    }

    private void Attack()
    {
        if (CurrentWeapon.CanAutoFire)
        {
            if (Input.GetMouseButton(0))
            {
                CurrentWeapon.Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurrentWeapon.Shoot();
            }
        }
        
    }

    public void SwitchWeapon()
    {
        CurrentWeapon.gameObject.SetActive(false);
        if (_currentGun >= _weapons.Length - 1)
        {
            _currentGun = 0;
        }
        else
        {
            _currentGun++;
        }
        
        CurrentWeapon = _weapons[_currentGun];
        _weapons[_currentGun].gameObject.SetActive(true);
    }
}
