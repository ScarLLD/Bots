using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _unitsCount;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;

    private Queue<Transform> _spawnPoints;
    private Tracker _tracker;

    private void Start()
    {
        _tracker = GetComponent<Tracker>();

        GenerateSpawnPoint();

        GenerateUnits();
    }

    private void GenerateSpawnPoint()
    {
        _spawnPoints = new Queue<Transform>();

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            _spawnPoints.Enqueue(_spawnPointsParent.GetChild(i));
        }
    }

    private void GenerateUnits()
    {
        int pointsCount = _spawnPoints.Count;

        for (int i = 0; i < _unitsCount && i < pointsCount; i++)
        {
            Unit unit = Instantiate(_unitPrefab, _spawnPoints.
                Dequeue().transform.position, Quaternion.identity, transform);

            _tracker.TakeUnit(unit);
        }
    }
}
