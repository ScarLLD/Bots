using UnityEngine;

[RequireComponent(typeof(UnitsBuyer))]
public class Base : MonoBehaviour
{
    [SerializeField] private Tracker _tracker;
    [SerializeField] private BaseButton _baseButton;

    private UnitsBuyer _unitsBuyer;
    private BaseCollector _baseCollector;
    private BaseBuilder _baseBuilder;
    private BuyUnitButtonView _buyUnitButtonView;
    private Flag _flag;

    private void Awake()
    {
        _unitsBuyer = GetComponent<UnitsBuyer>();
        _baseCollector = transform.parent.GetComponent<BaseCollector>();
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();
        _buyUnitButtonView = transform.parent.GetComponent<BuyUnitButtonView>();

        _unitsBuyer.Init(_baseCollector.GetWallet);
        _tracker.Init(_baseCollector.GetResourcePool);
        _baseButton.Init(_baseBuilder, _buyUnitButtonView);
    }

    public void SendUnit()
    {
        _tracker.
    }

    public void ChangeFlag(Flag flag)
    {
        _flag = flag;
    }
}
