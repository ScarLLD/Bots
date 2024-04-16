using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;

    private SheltersCollector _sheltersCollector;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;
    public bool TryBuyShelter => _sheltersCollector.TryBuyShelter();

    public void Init(SheltersCollector sheltersCollector, ResourcesStorage resourcesStorage)
    {
        _sheltersCollector = sheltersCollector;
        _employer.Init(resourcesStorage);
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
        _sheltersCollector.SpawnShelter(flag, unit);

        Flag = null;
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
