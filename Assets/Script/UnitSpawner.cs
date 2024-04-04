using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tracker))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _startUnitsCount;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;

    private Tracker _tracker;

    public bool IsFull { get; private set; } = false;

    public Queue<Transform> SpawnPoints { get; private set; }

    private void Awake()
    {
        _tracker = GetComponent<Tracker>();
    }

    private void Start()
    {
        GenerateSpawnPoint();

        GenerateUnits();
    }       

    private void GenerateSpawnPoint()
    {
        SpawnPoints = new Queue<Transform>();

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            SpawnPoints.Enqueue(_spawnPointsParent.GetChild(i));
        }
    }

    private void GenerateUnits()
    {
        int pointsCount = SpawnPoints.Count;

        for (int i = 0; i < _startUnitsCount && i < pointsCount; i++)
        {
            SpawnUnit();
        }
    }

    public void SpawnUnit()
    {
        Unit unit = Instantiate(_unitPrefab, SpawnPoints.
                  Dequeue().transform.position, Quaternion.identity, transform);

        _tracker.TakeUnit(unit);

        if (SpawnPoints.Count == 0)
            IsFull = true;
    }

    public void TakeUnit(Unit unit)
    {
        _uni
    }
}