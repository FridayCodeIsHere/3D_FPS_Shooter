using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;

    private int _currentHealth;
    public UnityEvent OnDead;
    public UnityEvent OnTakeDamage;
    

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_health < 0) _health = 0;
    }
    #endregion

    private void Start()
    {
        _currentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        damage = _currentHealth < damage ? _currentHealth : damage;
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            OnDead?.Invoke();
            return;
        }
        OnTakeDamage?.Invoke();
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    [ContextMenu("Random Health")]
    private void Randomize()
    {
        _health = Random.Range(50, 240);
    }
}
