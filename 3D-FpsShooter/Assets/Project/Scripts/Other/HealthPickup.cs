using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healAmount;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_healAmount < 0) _healAmount = 0;
    }
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IRegeneration regeneration))
        {
            regeneration.ApplyHealth(_healAmount, this.gameObject);
        }
    }
}
