using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitsAdder))]
public class Base : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Tracker _tracker;
    [SerializeField] private BaseButton _baseButton;

    private Coroutine _sendCoroutine;
    private Wallet _wallet;
    private UnitsAdder _unitsBuyer;
    private BaseCollector _baseCollector;
    private BaseBuilder _baseBuilder;
    private BuyUnitButtonView _buyUnitButtonView;
    private Flag _flag;

    private void Awake()
    {
        _unitsBuyer = GetComponent<UnitsAdder>();
        _baseCollector = transform.parent.GetComponent<BaseCollector>();
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();
        _buyUnitButtonView = transform.parent.GetComponent<BuyUnitButtonView>();
        _wallet = _baseCollector.GetWallet;

        _unitsBuyer.Init(_baseCollector.GetWallet);
        _tracker.Init(_baseCollector.GetResourcePool);
        _baseButton.Init(_baseBuilder, _buyUnitButtonView);
    }

    public void WaitRequire(Flag flag)
    {
        _flag = flag;
        _sendCoroutine ??= StartCoroutine(Wait());
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    private IEnumerator Wait()
    {
        bool isWork = true;

        while (isWork)
        {
            if (_wallet.GoldCount >= _baseBuilder.GetBasePrice
                && _tracker.TryGetUnit(out Unit unit))
            {
                unit.ComeFlag(_flag);
                isWork = false;
            }

            yield return null;
        }
    }
}
