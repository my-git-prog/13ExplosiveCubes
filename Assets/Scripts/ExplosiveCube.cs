using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private float _afterSplitMoveForce;
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 1f;
    public event UnityAction<ExplosiveCube> Clicked;

    public float SplitChance => _splitChance;

    private void OnMouseUpAsButton()
    {
        Clicked.Invoke(this);
    }

    public void Initialize(Vector3 scale, Vector3 forceDirection, float splitChance)
    {
        transform.localScale = scale;
        _rigidbody.AddForce(forceDirection * _afterSplitMoveForce);
        _splitChance = splitChance;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
