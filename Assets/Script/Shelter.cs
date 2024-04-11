using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Tracker _tracker;

    private BaseCollector _baseCollector;

    public UnitSpawner UnitSpawner => _unitSpawner;
    public Flag Flag { get; private set; }
    public int UnitsCount => _tracker.UnitsCount;
    public bool TryBuyBase => _baseCollector.TryBuyBase;

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();

        _tracker.Init(_baseCollector.ResourcePool);
    }

    public void SendBuildRequest(Flag flag)
    {
        _baseCollector.TakeFlag(flag);
        _tracker.TakeFlag(flag);

        Flag = flag;
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void BuildBase(Flag flag, Unit unit)
    {
        _baseCollector.GenerateBase(flag, unit);

        Flag = null;
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
