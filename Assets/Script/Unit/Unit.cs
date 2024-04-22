using System;
using UnityEngine;

[RequireComponent(typeof(UnitMover), typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private Employer _employer;
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;

    public Transform StartTransform => _unitMover.StartTransfrom;
    public bool IsBusy { get; private set; }

    private void OnEnable()
    {
        _unitMover.Arrived += _unitTaker.Interact;
        _unitTaker.ResourceTaken += _unitMover.MoveBack;
        _unitTaker.ResourceDelivered += ConfirmDelivery;
        _unitTaker.CameFlag += NotifyEmployer;
    }

    private void OnDisable()
    {
        _unitMover.Arrived -= _unitTaker.Interact;
        _unitTaker.ResourceTaken -= _unitMover.MoveBack;
        _unitTaker.ResourceDelivered -= ConfirmDelivery;
        _unitTaker.CameFlag -= NotifyEmployer;
    }

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();

        

        IsBusy = false;
    }

    public void Init(Transform tempTransform, Employer employer, int speed)
    {
        _employer = employer;

        _unitMover.Init(speed);
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
        _employer.ConfirmDelivery(gold);

        IsBusy = false;
    }

    public void ChangeBase(Transform tempTransfrom)
    {
        _unitMover.ChangeStartPosition(tempTransfrom);

        IsBusy = false;
    }

    private void NotifyEmployer(Flag flag)
    {
        _employer.NotifyShelter(this, flag);
    }
}