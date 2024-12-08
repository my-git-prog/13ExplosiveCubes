using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplosiveCube : MonoBehaviour
{
    private const int _minSubCubesCount = 2;
    private const int _maxSubCubesCount = 6;

    [SerializeField] private float _afterSplitMoveForce;
    [SerializeField] private ExplosiveCube _explosiveCubePrefab;
    [SerializeField] private Rigidbody _rigidbody;

    private float _splitChance = 1f;

    private void OnMouseUpAsButton()
    {
        Debug.Log("Click it!!!");

        if(Random.value <= _splitChance)
        {
            Split();
        }

        Destroy(gameObject);
    }

    private void CreateSubCube()
    {
        Instantiate(_explosiveCubePrefab, transform.position, Random.rotation).
            Initiate(transform.localScale / 2, Random.onUnitSphere, _splitChance / 2, Random.ColorHSV());
    }

    public void Initiate(Vector3 scale, Vector3 forceDirection, float splitChance, Color color)
    {
        transform.localScale = scale;
        _rigidbody.AddForce(forceDirection * _afterSplitMoveForce);
        _splitChance = splitChance;
        this.GetComponent<Renderer>().material.color = color;
    }

    private void Split()
    {
        for (int i = 0; i <= Random.Range(_minSubCubesCount, _maxSubCubesCount); i++)
        {
            CreateSubCube();
        }
    }
}
