using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    private Tracker _unitsTracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    private bool _isGrub = false;

    public bool IsGrub => _isGrub;

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

    public void StartGrub(Vector3 goldPosition)
    {
        ChangeGrubBool();
        _unitMover.MoveToPoint(goldPosition);
        _unitTaker.TakeGoldCord(goldPosition);
    }

    private void ConfirmDelivery(Resource gold)
    {
        _unitsTracker.ConfirmDelivery(gold);
        ChangeGrubBool();
    }

    public void ChangeGrubBool()
    {
        _isGrub = !_isGrub;
    }
}