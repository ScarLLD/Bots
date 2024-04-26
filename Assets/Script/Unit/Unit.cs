using UnityEngine;

[RequireComponent(typeof(UnitMover), typeof(UnitTaker))]
public class Unit : MonoBehaviour
{
    private UnitMover _unitMover;
    private UnitTaker _unitTaker;
    private Shelter _shelter;

    public Transform StartTransform { get; private set; }
    public bool IsBusy { get; private set; }

    private void OnEnable()
    {
        _unitTaker.CameFlag += SendNotify;
        _unitMover.Arrived += _unitTaker.Interact;
        _unitTaker.ResourceTaken += MoveBack;
        _unitTaker.ResourceDelivered += ConfirmDelivery;
    }

    private void OnDisable()
    {
        _unitTaker.CameFlag -= SendNotify;
        _unitMover.Arrived -= _unitTaker.Interact;
        _unitTaker.ResourceTaken -= MoveBack;
        _unitTaker.ResourceDelivered -= ConfirmDelivery;
    }

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unitTaker = GetComponent<UnitTaker>();

        IsBusy = false;
    }

    public void Init(Transform startTransform, Shelter shelter)
    {
        StartTransform = startTransform;
        _shelter = shelter;
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

    public void ConfirmDelivery(Resource resource)
    {
        _shelter.TakeGold(resource);

        IsBusy = false;
    }

    public void SendNotify(Flag flag)
    {
        _shelter.SendBuildRequest(this, flag);

        IsBusy = false;
    }

    public void MoveBack()
    {
        _unitMover.MoveToPoint(StartTransform);
    }
}