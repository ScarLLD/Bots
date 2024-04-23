using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Employer))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Shelter _shelter;
    [SerializeField] private Employer _employer;

    private Queue<Transform> _spawnPoints;

    public int GetSpawnPointsCount => _spawnPoints.Count;

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

    public void TakeSpawnPoint(Transform spawnPoint)
    {
        _spawnPoints.Enqueue(spawnPoint);
    }

    public void SpawnUnit()
    {
        Transform tempTransform = _spawnPoints.Dequeue();

        Unit unit = Instantiate(_unitPrefab, tempTransform.position, Quaternion.identity, transform);
        unit.Init(tempTransform, _shelter, _speed);

        _employer.TakeUnit(unit);
    }

    public void TakeUnit(Unit unit)
    {
        _employer.TakeUnit(unit);

        unit.transform.parent = transform;
        unit.ChangeBase(_spawnPoints.Dequeue());
    }
}