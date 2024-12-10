using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 1f;
    private float _explosionRadius;
    private float _explosionForce;
    public event Action<Vector3, Vector3, float, float, float> Splitting;

    private void OnMouseUpAsButton()
    {
        if (UnityEngine.Random.value <= _splitChance)
        {
            Splitting?.Invoke(transform.position, transform.localScale, _splitChance, _explosionRadius, _explosionForce);
        }
        else
        {
            Explode();
        }

        Destroy(gameObject);
    }

    public void Initialize(Vector3 scale, Vector3 forceDirection, float splitChance, float explosionRadius, float explosionForce)
    {
        transform.localScale = scale;
        _splitChance = splitChance;
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
        _rigidbody.AddForce(forceDirection * _explosionForce);
    }

    private void Explode()
    {
        foreach(Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if(hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
