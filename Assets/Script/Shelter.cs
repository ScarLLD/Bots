using Unity.VisualScripting;
using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;

    private SheltersCollector _sheltersCollector;
    private ResourceSpawner _resourceSpawner;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;
    public bool TryBuyShelter => _sheltersCollector.TryBuyShelter();
    public void SpawnUnit() => _unitSpawner.SpawnUnit();

    private void OnDisable()
    {
        _employer.ResourceDelievered -= _resourceSpawner.CollectResource;
    }

    public void Init(SheltersCollector sheltersCollector, ResourcesStorage resourcesStorage, ResourceSpawner resourceSpawner)
    {
        _sheltersCollector = sheltersCollector;
        _resourceSpawner = resourceSpawner;
        _employer.ResourceDelievered += _resourceSpawner.CollectResource;

        _employer.Init(resourcesStorage);
        _employer.SendUnitsWork();
    }

    public void SendBuildRequest(Flag flag)
    {
        _sheltersCollector.TakeFlag(flag);
        _employer.TakeFlag(flag);

        Flag = flag;
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void BuildBase(Flag flag, Unit unit)
    {
        _sheltersCollector.BuildNewShelter(flag, unit);

        Flag = null;
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
