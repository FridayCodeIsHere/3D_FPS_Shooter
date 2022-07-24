using System;


public delegate void DamageAction();
public interface IDamageable 
{
    public event DamageAction OnApplyDamage;
    public event DamageAction OnDead;
    public void ApplyDamage(int value);
}
