using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Tracker _unitsTracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public Resource GetTargetResource => _unitTaker.GetTargetResource;

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
    }

    public void ComeFlag(Flag flag)
    {
        _unitMover.MoveToPoint(flag.transform.position);
    }

    private void ConfirmDelivery(Resource gold)
    {
        _unitsTracker.ConfirmDelivery(gold);
    }
}