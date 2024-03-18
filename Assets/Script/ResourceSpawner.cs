using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetwenSpawn;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private ResourcePool _goldPool;

    private Transform[] _spawnPoints;
    private Coroutine _spawnCoroutine;
    private WaitForSeconds _wait;
    private bool _isGenerate;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetwenSpawn);
        _spawnPoints = new Transform[_spawnPointsParent.childCount];

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
    }

    private void Start()
    {
        GenerateGold();
    }

    private void GenerateGold()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        _isGenerate = true;

        while (_isGenerate)
        {
            List<Transform> _tempSpawnPoints = _spawnPoints.ToList();

            int goldCount = Random.Range(1, _tempSpawnPoints.Count);

            for (int i = 0; i < goldCount; i++)
            {
                int index = Random.Range(0, _tempSpawnPoints.Count - 1);
                Transform spawnPoint = _tempSpawnPoints[index];

                if (_goldPool.TryGetSpawnPoint(spawnPoint.position) == false)
                    _goldPool.GetGold(spawnPoint.position);

                _tempSpawnPoints.Remove(spawnPoint);
            }

            yield return _wait;
        }

        StopCoroutine(_spawnCoroutine);
    }
}