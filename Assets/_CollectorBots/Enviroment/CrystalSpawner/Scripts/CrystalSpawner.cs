using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] private Crystal _crystalPrefab;
    [SerializeField] private Vector2 _spawnArea;
    [SerializeField, Min(0)] private int _minCrystalSpawn;
    [SerializeField, Min(0)] private int _maxCrystalSpawn;
    [SerializeField, Min(0)] private float _spawnDelay;

    private List<Crystal> _crystals;

    private int _spawnDelayMiliseconds;

    private void OnValidate()
    {
        if (_minCrystalSpawn > _maxCrystalSpawn)
        {
            _minCrystalSpawn = _maxCrystalSpawn;
        }
    }

    private void Awake()
    {
        _crystals = new List<Crystal>();
        _spawnDelayMiliseconds = (int)_spawnDelay * 1000;
        SpawnCrystalsTask().Forget();
    }

    public List<Crystal> GetCrystals()
    {
        List<Crystal> cristals = new List<Crystal>(_crystals);
        _crystals.Clear();
        return cristals;
    }

    private async UniTaskVoid SpawnCrystalsTask()
    {
        while (this && enabled)
        {
            SpawnCrystals();
            
            await UniTask.Delay(_spawnDelayMiliseconds);
        }
    }

    private void SpawnCrystals()
    {
        int crystalCount = Random.Range(_minCrystalSpawn, _maxCrystalSpawn + 1);

        for (int i = 0; i < crystalCount; i++)
        {
            Crystal crystal = Instantiate(_crystalPrefab, GeneratePosition(), Quaternion.identity);
            _crystals.Add(crystal);
        }
    }

    private Vector3 GeneratePosition()
    {
        float x = Random.Range(transform.position.x - (_spawnArea.x / 2), transform.position.x + (_spawnArea.x / 2));
        float y = transform.position.y;
        float z = Random.Range(transform.position.z - (_spawnArea.y / 2), transform.position.z + (_spawnArea.y / 2));

        return new Vector3(x, y, z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(_spawnArea.x, 0, _spawnArea.y));
    }
}
