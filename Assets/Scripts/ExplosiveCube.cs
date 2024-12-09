using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    [SerializeField] private float _afterSplitMoveForce;
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 1f;
    public event UnityAction<float> Split;

    private void OnMouseUpAsButton()
    {
        if(Random.value <= _splitChance)
        {
            Split.Invoke(_splitChance);
        }

        Destroy(gameObject);
    }

    public void Initialize(Vector3 scale, Vector3 forceDirection, float splitChance)
    {
        transform.localScale = scale;
        _rigidbody.AddForce(forceDirection * _afterSplitMoveForce);
        _splitChance = splitChance;
    }

}
