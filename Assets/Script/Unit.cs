using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Tracker _tracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _tracker = transform.parent.GetComponent<Tracker>();
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();
    }

    public void StartGrub(Resource resource)
    {
        _unitMover.MoveToPoint(resource.transform.position);
        _unitTaker.ChooseTarget(resource);

        IsBusy = true;
    }

    public void ComeFlag(Flag flag)
    {
        _unitMover.MoveToPoint(flag.GetStartPosition.transform.position);
        

        IsBusy = true;
    }

    public void ConfirmDelivery(Resource gold)
    {
        _tracker.ConfirmDelivery(gold);

        IsBusy = false;
    }

    public void ChangeBase(Vector3 startPosition)
    {
        _unitMover.ChangeStartPosition(startPosition);

        IsBusy = false;
    }
}