using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private GoldPool _goldPool;
    [SerializeField] private Gold _goldPrefab;

    private Transform[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new Transform[_spawnPointsParent.childCount];

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
            _spawnPoints[i] = _spawnPointsParent.GetChild(i);
    }

    private void Start()
    {
        GenerateResources();
    }

    public void GenerateResources()
    {
        List<Transform> tempSpawnPoints = _spawnPoints.ToList();
        int goldCount = Random.Range(1, tempSpawnPoints.Count);

        for (int i = 0; i < goldCount; i++)
        {
            int index = Random.Range(0, tempSpawnPoints.Count - 1);
            Transform spawnPoint = tempSpawnPoints[index];

            _goldPool.GetGold(spawnPoint.position);

            tempSpawnPoints.Remove(spawnPoint);
        }
    }
}