using UnityEngine;


public delegate void ApplyHealth();

public interface IRegeneration 
{
    public bool OverflowMaxHealth { get; }
    public event ApplyHealth OnApplyHealth;
    public void ApplyHealth(int value, GameObject sender = null);
}
