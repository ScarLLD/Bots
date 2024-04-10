using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseCollector : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private ParticleSystem _particleShelterPrefab;
    [SerializeField] private Shelter _shelterPrefab;
    [SerializeField] private Vector3 _particleRotation;

    private List<Shelter> _shelters;
    private bool _isFlag;

    public ResourcePool ResourcePool => _resourcePool;
    public bool TryBuyBase => _wallet.GoldCount >= _shelterPrice;

    private void OnEnable()
    {
        _wallet.ScoreIncreased += BuyInterect;
    }

    private void OnDisable()
    {
        _wallet.ScoreIncreased -= BuyInterect;
    }

    private void Awake()
    {
        _shelters = new List<Shelter>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _shelters.Add(transform.GetChild(i).GetComponent<Shelter>());
        }
    }

    public void GenerateBase(Vector3 position, Unit unit)
    {
        Shelter shelter = Instantiate(_shelterPrefab, position, Quaternion.identity, transform);
        Instantiate(_particleShelterPrefab, shelter.transform.position,
            Quaternion.identity).transform.Rotate(_particleRotation); ;

        shelter.TakeUnit(unit);

        _shelters.Add(shelter);

        _wallet.DecreaseResources(_shelterPrice);

        _isFlag = false;
    }
       
    public void TakeFlag()
    {
        _isFlag = true;
    }

    private void BuyInterect()
    {
        if (_isFlag == false && _wallet.GoldCount >= _unitPrice)
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
