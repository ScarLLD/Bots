using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Employer))]
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private Unit _unitPrefab;

    private Employer _employer;

    public Queue<Transform> SpawnPoints { get; private set; }

    private void Awake()
    {
        _employer = GetComponent<Employer>();

        SpawnPoints = new Queue<Transform>();

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            SpawnPoints.Enqueue(_spawnPointsParent.GetChild(i));
        }
    }

    public void TakeSpawnPoint(Transform spawnPoint)
    {
        SpawnPoints.Enqueue(spawnPoint);
    }

    public void SpawnUnit()
    {
        Transform tempTransform = SpawnPoints.Dequeue();

        Unit unit = Instantiate(_unitPrefab, tempTransform.position, Quaternion.identity, transform);
        unit.Init(tempTransform, _employer, _speed);

        _employer.TakeUnit(unit);
    }

    public void TakeUnit(Unit unit)
    {
        _employer.TakeUnit(unit);

        unit.transform.parent = transform;
        unit.ChangeBase(SpawnPoints.Dequeue());
    }
}