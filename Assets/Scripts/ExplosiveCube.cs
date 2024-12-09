using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 1f;
    private float _explosionRadius;
    private float _explosionForce;
    public event UnityAction<ExplosiveCube> Clicked;

    public float SplitChance => _splitChance;
    public float ExplosionRadius => _explosionRadius;
    public float ExplosionForce => _explosionForce;

    private void OnMouseUpAsButton()
    {
        Clicked.Invoke(this);
    }

    public void Initialize(Vector3 scale, Vector3 forceDirection, float splitChance, float explosionRadius, float explosionForce)
    {
        transform.localScale = scale;
        _splitChance = splitChance;
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
        _rigidbody.AddForce(forceDirection * _explosionForce);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Explode()
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
