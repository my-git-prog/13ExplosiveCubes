using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosiveCube _explosiveCubePrefab;
    [SerializeField] private Vector3 _startSpawnPosition = new Vector3 (0, 2, 0);
    [SerializeField] private int _startCubesCount = 3;
    [SerializeField] private int _minSubCubesCount = 2;
    [SerializeField] private int _maxSubCubesCount = 6;
    [SerializeField] private float _startSplitChance = 1f;
    [SerializeField] private float _splitChanceMultiplier = 0.5f;
    [SerializeField] private float _startExplosionRadius = 2.5f;
    [SerializeField] private float _explosionRadiusMultiplier = 2f;
    [SerializeField] private float _startExplosionForce = 150f;
    [SerializeField] private float _explosionForceMultiplier = 2f;
    [SerializeField] private float _scaleMultiplier = 0.5f;

    private void Start()
    {
        CreateNewCubes(_startCubesCount, _startSpawnPosition, _explosiveCubePrefab.transform.localScale,
            _startSplitChance, _startExplosionRadius, _startExplosionForce);
    }

    private void SplitCube(ExplosiveCube cube)
    {
        RemoveListeners(cube);
        
        int count = Random.Range(_minSubCubesCount, _maxSubCubesCount + 1);
        
        CreateNewCubes(count, cube.transform.position, cube.transform.localScale * _scaleMultiplier, 
            cube.SplitChance * _splitChanceMultiplier, cube.ExplosionRadius * _explosionRadiusMultiplier, 
            cube.ExplosionForce * _explosionForceMultiplier);
    }

    private void CreateNewCubes(int count, Vector3 position, Vector3 scale, float splitChance, 
                                float explosionRadius, float explosionForce)
    {
        for (int i = 0; i < count; i++)
        {
            ExplosiveCube newCube = Instantiate(_explosiveCubePrefab, position, Random.rotation);

            newCube.Initialize(scale, splitChance, explosionRadius, explosionForce);
            newCube.AddForce(Random.onUnitSphere);
            newCube.Splitting += SplitCube;
            newCube.Exploding += RemoveListeners;
        }
    }

    private void RemoveListeners(ExplosiveCube cube)
    {
        cube.Splitting -= SplitCube;
        cube.Exploding -= RemoveListeners;
    }
}
