using System;
using UnityEngine;

[RequireComponent(typeof(UnitMover))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private UnitMover _unitMover;
    private Resource _targetResource;
    private Resource _tempResource;
    private bool _isBase = false;
    private bool _isGold = false;

    public event Action Taken;
    public event Action<Resource> Delivered;

    public Resource GetTargetResource => _targetResource;

    private void Awake()
    {
        _unitMover = GetComponent<UnitMover>();
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
