using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    private UnitsTracker _unitsTracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    private bool _isWalk = false;
    private bool _isGrub = false;

    public bool IsWalk => _isWalk;
    public bool IsGrub => _isGrub;

    private void Awake()
    {
        _unitsTracker = transform.parent.GetComponent<UnitsTracker>();
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();
    }

    private void OnEnable()
    {
        _unitTaker.Delivered += ConfirmDelivery;
        _unitsTracker.Extract += StartGrub;
    }

    public void StartGrub(Vector3 goldPosition)
    {
        _unitMover.MoveToPoint(goldPosition);
        _unitTaker.TakeGoldCord(goldPosition);
    }

    private void ConfirmDelivery(Gold gold)
    {

    }

    public void ChangeGrubStatus()
    {
        _isGrub = !_isGrub;
        Debug.Log(_isGrub);
    }
}