using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseCollector : MonoBehaviour
{
    [SerializeField] private int _basePrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Base _basePrefab;

    private Flag _flag;
    private List<Base> _bases;

    public ResourcePool GetResourcePool => _resourcePool;
    public Wallet GetWallet => _wallet;

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
        _bases = new List<Base>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _bases.Add(transform.GetChild(i).GetComponent<Base>());
        }
    }

    public void GenerateBase(Vector3 tempPosition, Unit unit)
    {
        Base tempBase = Instantiate(_basePrefab, tempPosition, Quaternion.identity, transform);
        _bases.Add(tempBase);

        tempBase.TakeUnit(unit);

        ClearFlag();
    }

    public bool TryBuyBase()
    {
        if (_wallet.GoldCount >= _basePrice)
        {
            _wallet.DecreaseResources(_basePrice);
            return true;
        }
        else
        {
            return false;

        }
    }

    public void TakeFlag(Flag flag)
    {
        _flag = flag;
    }

    public void ClearFlag()
    {
        _flag = null;
    }

    private void BuyInterect()
    {
        if (_flag == null && _wallet.GoldCount >= _unitPrice)
        {
            Base tempBase = _bases.FirstOrDefault(tempBase => tempBase.GetUnitSpawner.SpawnPoints.Count() > 0);

            if (tempBase != null)
            {
                tempBase.BuyUnit();
                _wallet.DecreaseResources(_unitPrice);
            }
        }
    }
}
