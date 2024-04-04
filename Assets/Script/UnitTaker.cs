using System;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
[RequireComponent(typeof(Unit))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private UnitMover _unitMover;
    private Unit _unit;
    private Resource _targetResource;
    private Resource _tempResource;
    private bool _isBase = false;
    private bool _isGold = false;
    private bool _isFlag = false;

    public event Action Taken;
    public event Action<Resource> Delivered;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _unit = GetComponent<Unit>();
    }

    private void OnEnable()
    {
        _unitMover.Arrived += Interact;
    }

    private void OnDisable()
    {
        _unitMover.Arrived -= Interact;
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
        else if (other.GetComponent<Flag>())
        {
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

    private void Interact()
    {
        if (_isBase && _tempResource != null)
        {
            PutGold();
        }
        else if (_isFlag)
        {
            _isFlag
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

        Taken?.Invoke();
    }

    private void PutGold()
    {
        Delivered?.Invoke(_tempResource);

        _tempResource = null;
        _targetResource = null;
    }
}
