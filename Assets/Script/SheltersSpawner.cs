using UnityEngine;

[RequireComponent(typeof(ResourcesStorage))]
public class SheltersSpawner : MonoBehaviour
{
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private ParticlePool _particlePool;
    [SerializeField] private Shelter _shelterPrefab;
    [SerializeField] private Camera _camera;

    private void Awake()
    {
        _resourcesStorage = GetComponent<ResourcesStorage>();
    }

    private void Start()
    {
        SpawnShelter(new Vector3(0, 0, 0));
    }

    public void BuildShelter(Unit unit, Flag flag)
    {
        Shelter shelter = SpawnShelter(new Vector3(flag.transform.position.x, 0, flag.transform.position.z));
        shelter.TakeUnit(unit);
    }

    private Shelter SpawnShelter(Vector3 spawnPosition)
    {
        Shelter shelter = Instantiate(_shelterPrefab, spawnPosition, Quaternion.identity, transform);
        shelter.Init(_resourcesStorage, _resourcePool, this, _camera);

        ParticleSystem particle = _particlePool.GetParticle();
        particle.transform.position = shelter.transform.position;
        particle.gameObject.SetActive(true);

        return shelter;
    }
}
