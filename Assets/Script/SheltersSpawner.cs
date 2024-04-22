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
    [SerializeField] private SheltersStorage _sheltersStorage;

    public event Action<Flag> FlagRemoved;
    public event Action ShelterBuilded;

    private void Awake()
    {
        _resourcesStorage = GetComponent<ResourcesStorage>();
    }

    private void Start()
    {
        SpawnShelter(_firstBasePosition).SpawnUnit();
    }

    public void BuildShelter(Unit unit, Flag flag)
    {
        Shelter shelter = SpawnShelter(flag.transform.position);
        shelter.TakeUnit(unit);

        FlagRemoved?.Invoke(flag);
        ShelterBuilded?.Invoke();
    }

    private Shelter SpawnShelter(Vector3 spawnPosition)
    {
        Shelter shelter = Instantiate(_shelterPrefab, spawnPosition, Quaternion.identity, transform);
        shelter.Init(_resourceSpawner, _resourcesStorage, _sheltersBuyer, this);

        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;

        _sheltersStorage.PutShelter(shelter);

        return shelter;
    }
}
