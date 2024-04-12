using UnityEngine;

[RequireComponent(typeof(UnitMover), typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Tracker _tracker;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public Transform StartTransform => _unitMover.StartTransfrom;
    public bool IsBusy { get; private set; }

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();

        _unitTaker.Init();
    }

    public void Init(Transform tempTransform, Tracker tracker)
    {
        _tracker = tracker;
        _unitMover.ChangeStartPosition(tempTransform);
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

    public void Interact()
    {
        if (_unitTaker.TempFlag != null)
        {
            _tracker.BuildBase(_unitTaker.TempFlag, this);
            _unitTaker.ClearFlag();
        }
        else if (_unitTaker.TempShelter != null)
        {
            _unitTaker.PutGold();
        }
        else if (_unitTaker.TempResource != null)
        {
            _unitTaker.TakeGold();
            _unitMover.MoveBack();
        }
    }
}