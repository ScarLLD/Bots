using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Tracker _tracker;

    private BaseCollector _baseCollector;

    public int GetUnitsCount => _tracker.GetUnitsCount;
    public UnitSpawner GetUnitSpawner => _unitSpawner;
    public bool TryBuyBase => _baseCollector.TryBuyBase();

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();

        _tracker.Init(_baseCollector.GetResourcePool);
    }

    public void SendBuildRequest(Flag flag)
    {
        _baseCollector.TakeFlag(flag);
        _tracker.TakeFlag(flag);
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void BuildBase(Flag flag, Unit unit)
    {
        _baseCollector.GenerateBase(flag.transform.position, unit);

        Destroy(flag.gameObject);
    }

    public void BuyUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
