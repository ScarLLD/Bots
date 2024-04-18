using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;

    private SheltersSpawner _sheltersCollector;
    private FlagStorage _flagStorage;
    private Wallet _wallet;
    private ResourceSpawner _resourceSpawner;
    private ResourcesStorage _resourcesStorage;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;
    public void SpawnUnit() => _unitSpawner.SpawnUnit();

    private void Awake()
    {
        _employer.Init(_resourcesStorage, _flagStorage, _wallet);
        _employer.SendUnitsWork();
    }

    private void OnEnable()
    {
        _employer.ResourceDelivered += TransferResource;
    }

    private void OnDisable()
    {
        _employer.ResourceDelivered -= TransferResource;
    }

    public void Init(SheltersSpawner sheltersCollector, ResourcesStorage resourcesStorage, ResourceSpawner resourceSpawner)
    {
        _sheltersCollector = sheltersCollector;
        _resourceSpawner = resourceSpawner;
        _resourcesStorage = resourcesStorage;
    }

    private void TransferResource(Resource resource)
    {
        _resourceSpawner.CollectResource(resource);
    }

    public void SendBuildRequest(Flag flag)
    {
        _employer.TakeFlag(flag);

        Flag = flag;
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
