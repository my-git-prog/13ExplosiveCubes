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

    private void Start()
    {
        CreateNewCubes(_startCubesCount, _startSpawnPosition, _explosiveCubePrefab.transform.localScale,
            _startSplitChance, _startExplosionRadius, _startExplosionForce);
    }

    private void SplitCube(Vector3 position, Vector3 scale, float splitChance, float explosionRadius, float explosionForce)
    {
        int count = Random.Range(_minSubCubesCount, _maxSubCubesCount + 1);
        
        CreateNewCubes(count, position, scale / 2, splitChance / 2, explosionRadius * 2, explosionForce * 2);
    }

    private void CreateNewCubes(int count, Vector3 position, Vector3 scale, float splitChance, 
                                float explosionRadius, float explosionForce)
    {
        for (int i = 0; i < count; i++)
        {
            ExplosiveCube newCube = Instantiate(_explosiveCubePrefab, position, Random.rotation);

            newCube.Initialize(scale, Random.onUnitSphere, splitChance, explosionRadius, explosionForce);
            newCube.Splitting += SplitCube;
        }
    }
}
