using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetwenSpawn;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private GoldPool _goldPool;
    [SerializeField] private Gold _goldPrefab;

    private List<Transform> _tempSpawnPoints;
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

        _tempSpawnPoints = _spawnPoints.ToList();
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
            int goldCount = Random.Range(1, _tempSpawnPoints.Count);

            for (int i = 0; i < goldCount; i++)
            {
                int index = Random.Range(0, _tempSpawnPoints.Count - 1);
                Transform spawnPoint = _tempSpawnPoints[index];

                if (_goldPool.TryGetPoint(spawnPoint.position))
                    _goldPool.GetGold(spawnPoint.position);

                _tempSpawnPoints.Remove(spawnPoint);

                //проверка наличие свободных мест для золота, но золото не появлвяется!
            }

            yield return _wait;
        }

        StopCoroutine(_spawnCoroutine);
    }
}