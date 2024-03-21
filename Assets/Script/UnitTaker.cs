using System;
using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    private UnitMover _unitMover;
    private Resource _targetResource;
    private Resource _tempResource;
    private bool _isBase = false;
    private bool _isGold = false;
    private Vector3 _objectPosition;

    public event Action Taken;
    public event Action<Resource> Delivered;

    public Resource GetTargetResource => _targetResource;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
        _objectPosition = transform.GetChild(0).localPosition;
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
        if (other.gameObject.TryGetComponent(out Resource gold))
        {
            if (gold == _targetResource)
            {
                _isGold = true;
                _tempResource = gold;
            }
        }
        else if (other.gameObject.GetComponent<Base>())
        {
            _isBase = true;
        }
    }

    private void OnTriggerExit()
    {
        _isGold = false;
        _isBase = false;
    }

    public void ChooseTarget(Resource resource)
    {
        _targetResource = resource;
    }

    private void Interact()
    {
        if (_isBase && _tempResource != null)
        {
            Delivered?.Invoke(_tempResource);

            _tempResource = null;
            _targetResource = null;
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
}
