using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Tracker _unitsTracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _unitsTracker = transform.parent.GetComponent<Tracker>();
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();
    }

    private void OnEnable()
    {
        _unitTaker.Delivered += ConfirmDelivery;
    }

    private void OnDisable()
    {
        _unitTaker.Delivered -= ConfirmDelivery;
    }

    public void StartGrub(Resource resource)
    {
        resource.ChangeGrubBool();
        _unitMover.MoveToPoint(resource.transform.position);
        _unitTaker.ChooseTarget(resource);

        IsBusy = true;
    }

    public void ComeFlag(Flag flag)
    {
        _unitMover.MoveToPoint(flag.transform.position);

        IsBusy = true;
    }

    private void ConfirmDelivery(Resource gold)
    {
        _unitsTracker.ConfirmDelivery(gold);

        IsBusy = true;
    }
}