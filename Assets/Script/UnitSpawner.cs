using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Shelter _shelter;
    [SerializeField] private Employer _employer;
    [SerializeField] private UnitsStorage _unitsStorage;

    private Queue<Transform> _spawnPoints;

    public Transform GetSpawnPoint => _spawnPoints.Dequeue();
    public int GetSpawnPointsCount => _spawnPoints.Count;

    private void OnEnable()
    {
        _employer.UnitCameFlag += TakeSpawnPoint;
    }
    private void OnDisable()
    {
        _employer.UnitCameFlag -= TakeSpawnPoint;
    }

    private void Awake()
    {
        _spawnPoints = new Queue<Transform>();

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            _spawnPoints.Enqueue(_spawnPointsParent.GetChild(i));
        }
    }

    private void Start()
    {
        SpawnUnit();
    }

    public void TakeSpawnPoint(Unit unit)
    {
        _spawnPoints.Enqueue(unit.StartTransform);
    }

    public void SpawnUnit()
    {
        Transform startTransforn = _spawnPoints.Dequeue();

        Unit unit = Instantiate(_unitPrefab, startTransforn.position, Quaternion.identity, transform);
        unit.Init(startTransforn, _shelter);

        _unitsStorage.TakeUnit(unit);
    }
}