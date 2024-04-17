using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ResourcesStorage))]
public class SheltersCollector : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ParticleSystem _particleShelterPrefab;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private ResourcesStorage _resourcesStorage;
    [SerializeField] private Shelter _shelterPrefab;
    [SerializeField] private Vector3 _particleRotation;
    [SerializeField] private Vector3 _firstBasePosition;

    private List<Shelter> _shelters;
    private List<Flag> _flags;

    public ResourceSpawner ResourceSpawner => _resourceSpawner;

    private void OnEnable()
    {
        _wallet.ScoreChanged += BuyInterect;
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= BuyInterect;
    }

    private void Awake()
    {
        _resourcesStorage = GetComponent<ResourcesStorage>();

        _shelters = new List<Shelter>();
        _flags = new List<Flag>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _shelters.Add(transform.GetChild(i).GetComponent<Shelter>());
        }

        SpawnShelter(_firstBasePosition).SpawnUnit();
    }

    public bool TryBuyShelter()
    {
        return _wallet.GoldCount >= _shelterPrice * _flags.Count;
    }

    public void BuildNewShelter(Flag flag, Unit unit)
    {
        Shelter shelter = SpawnShelter(flag.transform.position);

        shelter.TakeUnit(unit);

        _wallet.DecreaseResources(_shelterPrice);

        _flags.Remove(flag);
        Destroy(flag.gameObject);
    }

    private Shelter SpawnShelter(Vector3 spawnPosition)
    {
        Shelter shelter = Instantiate(_shelterPrefab, spawnPosition, Quaternion.identity, transform);
        shelter.Init(this, _resourcesStorage, _resourceSpawner);

        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;

        _shelters.Add(shelter);

        return shelter;
    }

    public void TakeFlag(Flag flag)
    {
        _flags.Add(flag);
    }

    private void BuyInterect()
    {
        if (_flags.Count == 0 && _wallet.GoldCount >= _unitPrice)
        {
            Shelter shelter = _shelters.FirstOrDefault(shelter => shelter.UnitSpawner.SpawnPoints.Count() > 0);

            if (shelter != null)
            {
                shelter.BuyUnit();
                _wallet.DecreaseResources(_unitPrice);
            }
        }
    }
}
