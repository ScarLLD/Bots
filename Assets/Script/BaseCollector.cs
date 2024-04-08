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

    private BaseBuilder _baseBuilder;
    private List<Base> _bases;

    public ResourcePool GetResourcePool => _resourcePool;

    private void OnEnable()
    {
        _wallet.ScoreChanged += TryBuyUnit;
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= TryBuyUnit;
    }

    private void Awake()
    {
        _baseBuilder = GetComponent<BaseBuilder>();
        _bases = new List<Base>();
    }

    private void Start()
    {
        GenerateBase();
    }

    public void GenerateBase()
    {
        _bases.Add(Instantiate(_basePrefab, transform));
    }

    private void TryBuyUnit()
    {
        if (_baseBuilder.GetTempFlag() == null)
        {
            Base tempBase = _bases.FirstOrDefault(tempBase => tempBase.GetUnitSpawner.SpawnPoints.Count() > 3); ;

            if (tempBase != null)
            {
                Debug.Log("buyed");

                tempBase.BuyUnit();
                _wallet.DecreaseResources(_unitPrice);
            }
        }
    }
}
