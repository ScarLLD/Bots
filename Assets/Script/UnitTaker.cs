using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(Unit))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private Tracker _tracker;
    private UnitMover _unitMover;
    private Unit _unit;
    private Flag _tempFlag;
    private Resource _targetResource;
    private Resource _tempResource;
    private bool _isBase = false;

    private void Awake()
    {
        _tracker = transform.parent.GetComponent<Tracker>();
        _unitMover = GetComponent<UnitMover>();
        _unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Shelter>())
        {
            _isBase = true;
        }
        else if (other.TryGetComponent(out Resource gold))
        {
            if (gold == _targetResource)
            {                
                _tempResource = gold;
            }
        }
        else if (other.TryGetComponent<Flag>(out Flag flag))
        {
            _tempFlag = flag;
        }
    }

    private void OnTriggerExit()
    {
        _isBase = false;
    }

    public void ChooseTarget(Resource resource)
    {
        resource.ChangeGrubBool();
        _targetResource = resource;
    }

    public void Interact()
    {
        if (_tempFlag != null)
        {
            BuildBase();
        }
        else if (_isBase)
        {
            PutGold();
        }
        else if (_tempResource != null)
        {
            TakeGold();
        }
    }

    private void TakeGold()
    {
        _tempResource.transform.parent = transform;
        _tempResource.transform.localPosition = _objectPosition;

        _unitMover.MoveBack();
    }

    private void PutGold()
    {
        _unit.ConfirmDelivery(_tempResource);

        _tempResource = null;
        _targetResource = null;
    }

    private void BuildBase()
    {
        _tracker.BuildBase(_tempFlag, _unit);
        _tempFlag = null;
    }
}
