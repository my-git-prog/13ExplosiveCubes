using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosiveCube _explosiveCubePrefab;
    [SerializeField] private Vector3 _startSpawnPosition = new Vector3 (0, 2, 0);
    [SerializeField] private int _startCubesCount = 3;
    [SerializeField] private int _minSubCubesCount = 2;
    [SerializeField] private int _maxSubCubesCount = 6;
    [SerializeField] private float _startSplitChance = 1f;
    [SerializeField] private float _startExplosionRadius = 2.5f;
    [SerializeField] private float _startExplosionForce = 150f;

    private List<ExplosiveCube> _explosiveCubes = new List<ExplosiveCube>();

    private void Start()
    {
        CreateNewCubes(_startCubesCount, _startSpawnPosition, _explosiveCubePrefab.transform.localScale,
            _startSplitChance, _startExplosionRadius, _startExplosionForce);
    }

    private void OnDisable()
    {
        foreach(ExplosiveCube cube in _explosiveCubes)
        {
            cube.Clicked -= DestroyClickedCube;
        }
    }

    private void DestroyClickedCube(ExplosiveCube cube)
    {
        if(Random.value <= cube.SplitChance)
        {
            int count = Random.Range(_minSubCubesCount, _maxSubCubesCount + 1);

            CreateNewCubes(count, cube.transform.position, cube.transform.localScale / 2, cube.SplitChance / 2,
                cube.ExplosionRadius * 2, cube.ExplosionForce * 2);
        }
        else
        {
            cube.Explode();
        }

        RemoveCube(cube);
    }

    private void RemoveCube(ExplosiveCube cube)
    {
        _explosiveCubes.Remove(cube);
        cube.Clicked -= DestroyClickedCube;
        cube.Destroy();
    }

    private void CreateNewCubes(int count, Vector3 position, Vector3 scale, float splitChance, 
                                float explosionRadius, float explosionForce)
    {
        for (int i = 0; i < count; i++)
        {
            ExplosiveCube newCube = Instantiate(_explosiveCubePrefab, position, Random.rotation);

            newCube.Initialize(scale, Random.onUnitSphere, splitChance, explosionRadius, explosionForce);
            newCube.Clicked += DestroyClickedCube;
            _explosiveCubes.Add(newCube);
        }
    }
}
