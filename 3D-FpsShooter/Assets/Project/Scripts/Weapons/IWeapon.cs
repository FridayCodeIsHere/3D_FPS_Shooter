

public delegate void WeaponAction(int value);
public interface IWeapon 
{
    public event WeaponAction OnShoot;
    public event WeaponAction OnApplyAmmo;
    public int CountAmmo { get; }
    public float ReloadTime { get; }
    public bool CanFire { get; }
    public bool CanAutoFire { get; }
    public void Shoot();
    public void ApplyAmmo(int value);
    
}
