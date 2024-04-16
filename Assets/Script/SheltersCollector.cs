using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SheltersCollector : MonoBehaviour
{
    [SerializeField] private int _startUnitsCount;
    [SerializeField] private int _shelterPrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ParticleSystem _particleShelterPrefab;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private Shelter _shelterPrefab;
    [SerializeField] private Vector3 _particleRotation;

    private ResourcesStorage _resourcesStorage;
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
        _shelters = new List<Shelter>();
        _flags = new List<Flag>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _shelters.Add(transform.GetChild(i).GetComponent<Shelter>());
        }

        Shelter shelter = Instantiate(_shelterPrefab, flag.transform.position, Quaternion.identity, transform);
        shelter.TakeUnit(Insta)
    }

    public bool TryBuyShelter()
    {
        return _wallet.GoldCount >= _shelterPrice * _flags.Count;
    }

    public void SpawnShelter(Flag flag, Unit unit)
    {
        Shelter shelter = Instantiate(_shelterPrefab, flag.transform.position, Quaternion.identity, transform);
        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;
        shelter.Init(this, _resourcesStorage);
        shelter.TakeUnit(unit);
        _shelters.Add(shelter);
        _wallet.DecreaseResources(_shelterPrice);
        _flags.Remove(flag);
        Destroy(flag.gameObject);
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
