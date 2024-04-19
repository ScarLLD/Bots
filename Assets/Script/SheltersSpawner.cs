using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(ResourcesStorage))]
public class SheltersSpawner : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Vector3 _particleRotation;
    [SerializeField] private Vector3 _firstBasePosition;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private SheltersBuyer _sheltersBuyer;
    [SerializeField] private FlagsStorage _flagStorage;
    [SerializeField] private ParticleSystem _particleShelterPrefab;
    [SerializeField] private Shelter _shelterPrefab;

    private List<Shelter> _shelters;

    public event Action<Flag> FlagRemoved;
    public event Action ShelterBuilded;

    private void Awake()
    {
        _resourcesStorage = GetComponent<ResourcesStorage>();
        _shelters = new List<Shelter>();
    }

    private void Start()
    {
        SpawnShelter(_firstBasePosition).SpawnUnit();
    }

    public bool TryChooseShelter(out Shelter shelter)
    {
        shelter = _shelters.FirstOrDefault(shelter => shelter.UnitSpawner.SpawnPoints.Count() > 0);

        return shelter != null;
    }

    public void BuildShelter(Flag flag, Unit unit)
    {
        Shelter shelter = SpawnShelter(flag.transform.position);
        shelter.TakeUnit(unit);

        FlagRemoved?.Invoke(flag);
        ShelterBuilded?.Invoke();
    }

    private Shelter SpawnShelter(Vector3 spawnPosition)
    {
        Shelter shelter = Instantiate(_shelterPrefab, spawnPosition, Quaternion.identity, transform);
        shelter.Init(_resourceSpawner, _resourcesStorage, _sheltersBuyer);

        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;

        _shelters.Add(shelter);

        return shelter;
    }
}
