using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(Clicker))]
public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Clicker clicker;

    private float _splitChance = 1f;
    private float _explosionRadius;
    private float _explosionForce;
    public event Action<ExplosiveCube> Splitting;
    public event Action<ExplosiveCube> Exploding;

    public float SplitChance => _splitChance;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

    private void OnEnable()
    {
        clicker.Clicked += Explode;
    }

    private void OnDisable()
    {
        clicker.Clicked -= Explode;
    }

    private void Explode()
    {
        if (UnityEngine.Random.value <= _splitChance)
        {
            Splitting?.Invoke(this);
        }
        else
        {
            Exploding?.Invoke(this);
            _exploder.Explode(_explosionRadius, _explosionForce);
        }

        Destroy(gameObject);
    }

    public void Initialize(Vector3 scale, float splitChance, float explosionRadius, float explosionForce)
    {
        transform.localScale = scale;
        _splitChance = splitChance;
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
    }

    public void AddForce(Vector3 forceDirection)
    {
        _rigidbody.AddForce(forceDirection * _explosionForce);
    }
}
