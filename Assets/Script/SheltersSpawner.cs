using UnityEngine;
using System;

[RequireComponent(typeof(ResourcesStorage))]
public class SheltersSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 _particleRotation;
    [SerializeField] private Vector3 _firstBasePosition;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private FlagStorage _flagStorage;
    [SerializeField] private ParticleSystem _particleShelterPrefab;
    [SerializeField] private Shelter _shelterPrefab;
    [SerializeField] private SheltersStorage _sheltersStorage;
    [SerializeField] private Camera _camera;

    public event Action ShelterBuilded;

    private void Awake()
    {
        _resourcesStorage = GetComponent<ResourcesStorage>();
    }

    private void Start()
    {
        SpawnShelter(_firstBasePosition);
    }

    public void BuildShelter(Unit unit, Flag flag)
    {
        Shelter shelter = SpawnShelter(flag.transform.position);
        shelter.TakeUnit(unit);

        ShelterBuilded?.Invoke();
    }

    private Shelter SpawnShelter(Vector3 spawnPosition)
    {
        Shelter shelter = Instantiate(_shelterPrefab, spawnPosition, Quaternion.identity, transform);
        shelter.Init(_resourceSpawner, _resourcesStorage, this, _camera);

        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;

        _sheltersStorage.PutShelter(shelter);

        return shelter;
    }
}
