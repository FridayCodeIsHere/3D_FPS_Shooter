using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private int _damage;
    [SerializeField] private string _nameLayerCollision;
    private Rigidbody _rb;
    private Collider _collider;
    private float _multiplierSpeed = 200f;

    #region MonoBehaviour
    private void OnValidate()
    {
        if (_speed < 0) _speed = 0;
        if (_lifeTime < 0) _lifeTime = 0;
        if (_damage < 0) _damage = 0;
    }
    #endregion

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        _rb.velocity = transform.forward * _speed * Time.deltaTime * _multiplierSpeed;

        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(_nameLayerCollision))
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageableItem))
            {

                damageableItem.ApplyDamage(_damage);
            }
        }
        Destroy(this.gameObject);
        Instantiate(_particleSystem, transform.position + (transform.forward *(-_speed * Time.deltaTime)), transform.rotation);
    }
}
