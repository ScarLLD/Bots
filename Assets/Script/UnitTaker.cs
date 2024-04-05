using System;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(Unit))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private UnitMover _unitMover;
    private Unit _unit;
    private Flag _tempFlag;
    private Resource _targetResource;
    private Resource _tempResource;
    private bool _isBase = false;
    private bool _isGold = false;
    private bool _isFlag = false;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Base>())
        {
            _isBase = true;
        }
        else if (other.TryGetComponent(out Resource gold))
        {
            if (gold == _targetResource)
            {
                _isGold = true;
                _tempResource = gold;
            }
        }
        else if (other.TryGetComponent<Flag>(out Flag flag))
        {
            _tempFlag = flag;
            _isFlag = true;
        }
    }

    private void OnTriggerExit()
    {
        _isGold = false;
        _isBase = false;
        _isFlag = false;
    }

    public void ChooseTarget(Resource resource)
    {
        _targetResource = resource;
    }

    public void Interact()
    {
        if (_isBase && _tempResource != null)
        {
            PutGold();
        }
        else if (_isFlag)
        {
            BuildBase();
        }
        else if (_isGold)
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
        _tempFlag.SpawnBase(_unit);
    }
}
