using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tracker))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _startUnitsCount;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;

    private Tracker _tracker;

    public Queue<Transform> SpawnPoints { get; private set; }
    public bool IsFull { get; private set; } = false;

    private void Awake()
    {
        _tracker = GetComponent<Tracker>();

        GenerateSpawnPoint();
    }

    private void Start()
    {
        GenerateUnits();
    }

    public void TakeSpawnPoint(Transform spawnPoint)
    {
        SpawnPoints.Enqueue(spawnPoint);
    }

    public void SpawnUnit()
    {
        Transform tempTransform = SpawnPoints.Dequeue();

        Unit unit = Instantiate(_unitPrefab, tempTransform.position, Quaternion.identity, transform);
        unit.Init(tempTransform);

        _tracker.TakeUnit(unit);
    }

    public void TakeUnit(Unit unit)
    {
        _tracker.TakeUnit(unit);

        unit.transform.parent = transform;
        unit.ChangeBase(SpawnPoints.Dequeue());
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
        for (int i = 0; i < _startUnitsCount && SpawnPoints.Count > 0; i++)
        {
            SpawnUnit();
        }
    }
}