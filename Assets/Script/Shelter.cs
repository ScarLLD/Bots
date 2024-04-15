using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Tracker _tracker;

    private SheltersCollector _shelterCollector;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _tracker.UnitsCount;
    public bool TryBuyShelter => _shelterCollector.TryBuyShelter();

    private void Awake()
    {
        _shelterCollector = transform.parent.GetComponent<SheltersCollector>();

        _tracker.Init(_shelterCollector.ResourceSpawner);
    }

    public void SendBuildRequest(Flag flag)
    {
        _shelterCollector.TakeFlag(flag);
        _tracker.TakeFlag(flag);

        Flag = flag;
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void BuildBase(Flag flag, Unit unit)
    {
        _shelterCollector.SpawnShelter(flag, unit);

        Flag = null;
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
