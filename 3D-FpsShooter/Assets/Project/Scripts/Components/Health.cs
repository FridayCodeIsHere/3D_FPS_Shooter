using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable, IRegeneration
{
    [SerializeField] private int _health;
    [SerializeField] private bool _overflowMaxHealth;
    public int CurrentHealth { get; private set; }

    public int TotalHealth => _health;
    public bool OverflowMaxHealth => _overflowMaxHealth;

    public event DamageAction OnApplyDamage;
    public event DamageAction OnDead;
    public event ApplyHealth OnApplyHealth;


    #region MonoBehaviour
    private void OnValidate()
    {
        if (_health < 0) _health = 0;
    }
    #endregion

    private void OnEnable()
    {
        OnDead += DestroyObject;
    }

    private void OnDisable()
    {
        OnDead -= DestroyObject;
    }

    private void Awake()
    {
        CurrentHealth = _health;
    }

    public void ApplyDamage(int damage)
    {
        damage = CurrentHealth < damage ? CurrentHealth : damage;
        CurrentHealth -= damage;
        OnApplyDamage?.Invoke();

        if (CurrentHealth <= 0)
        {
            OnDead?.Invoke();
        }
    }

    public void ApplyHealth(int healAmount, GameObject sender = null)
    {
        if (OverflowMaxHealth)
        {
            CurrentHealth += healAmount;
            _health = CurrentHealth;
            OnApplyHealth?.Invoke();
            if (sender)
                Destroy(sender);
        }
        else
        {
            if (CurrentHealth < TotalHealth)
            {
                int differenceHealth = _health - CurrentHealth;
                healAmount = differenceHealth > healAmount ? healAmount : differenceHealth;
                CurrentHealth += healAmount;
                OnApplyHealth?.Invoke();
                if (sender)
                    Destroy(sender);
            }
        }
    }

    public void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    [ContextMenu("Random Health")]
    private void Randomize()
    {
        _health = Random.Range(50, 240);
    }
}
