using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int _countAmount;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_countAmount < 0) _countAmount = 0;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IWeapon weapon))
        {
            weapon.ApplyAmmo(_countAmount);
            Destroy(this.gameObject);
        }
    }
}
