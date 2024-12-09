using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosiveCube _explosiveCubePrefab;
    [SerializeField] private ExplosiveCube _explosiveCube;
    [SerializeField] private const int _minSubCubesCount = 2;
    [SerializeField] private const int _maxSubCubesCount = 6;

    private void OnEnable()
    {
        _explosiveCube.Split += CreateNewCubes;
    }

    private void OnDisable()
    {
        _explosiveCube.Split -= CreateNewCubes;
    }

    private void CreateNewCubes(float splitChance)
    {
        for (int i = 0; i <= Random.Range(_minSubCubesCount, _maxSubCubesCount); i++)
        {
            Instantiate(_explosiveCubePrefab, transform.position, Random.rotation).
                Initialize(transform.localScale / 2, Random.onUnitSphere, splitChance / 2);
        }
    }
}
