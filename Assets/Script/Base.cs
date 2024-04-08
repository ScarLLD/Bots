using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Tracker _tracker;
    [SerializeField] private BaseButton _baseButton;

    private BaseCollector _baseCollector;
    private BaseBuilder _baseBuilder;

    public UnitSpawner GetUnitSpawner => _unitSpawner;

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();

        _tracker.Init(_baseCollector.GetResourcePool);
        _baseButton.Init(_baseBuilder);
    }

    public void TakeFlag(Flag flag)
    {
        _tracker.TakeFlag(flag);
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
