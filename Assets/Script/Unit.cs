using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Tracker _tracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public Transform StartTransform => _unitMover.StartTransfrom;
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _tracker = transform.parent.GetComponent<Tracker>();
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();
    }

    public void StartGrub(Resource resource)
    {
        _unitMover.MoveToPoint(resource.transform);
        _unitTaker.ChooseTarget(resource);

        IsBusy = true;
    }

    public void ComeFlag(Flag flag)
    {
        _unitMover.MoveToPoint(flag.transform);

        IsBusy = true;
    }

    public void Init(Transform tempTransform)
    {
        _unitMover.ChangeStartPosition(tempTransform);
    }

    public void ConfirmDelivery(Resource gold)
    {
        _tracker.ConfirmDelivery(gold);

        IsBusy = false;
    }

    public void ChangeBase(Transform tempTransfrom)
    {
        _unitMover.ChangeStartPosition(tempTransfrom);

        IsBusy = false;
    }
}