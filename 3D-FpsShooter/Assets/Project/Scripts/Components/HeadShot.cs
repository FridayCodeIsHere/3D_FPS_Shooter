using UnityEngine;

public class HeadShot : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    public event DamageAction OnApplyDamage;
    public event DamageAction OnDead;

    public void ApplyDamage(int damage)
    {
        _health.ApplyDamage(damage * 2);
        OnApplyDamage?.Invoke();

        if (_health.CurrentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }
    
}
