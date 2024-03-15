using System;
using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    private UnitMover _unitMover;
    private Gold _gold;
    private Vector3 _currentGoldPosition;
    private bool _isBase = false;
    private bool _isGold = false;

    public event Action Taken;
    public event Action<Gold> Delivered;

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
        if (other.gameObject.TryGetComponent(out Gold gold) && gold.IsTaken == false)
        {
            if (gold.transform.position == _currentGoldPosition)
            {
                _isGold = true;
                _gold = gold;
            }
        }
        else if (other.gameObject.GetComponent<CollectingZone>())
        {
            _isBase = true;
        }
    }

    private void OnTriggerExit()
    {
        _isGold = false;
        _isBase = false;

        _gold = null;
    }

    public void TakeGoldCord(Vector3 goldPosition)
    {
        _currentGoldPosition = goldPosition;
    }

    private void Interact()
    {        
        if (_isBase && _gold != null)
        {
            Delivered?.Invoke(_gold);
            _gold = null;
        }
        else if (_isGold)
        {
            TakeGold();
        }
    }

    private void TakeGold()
    {
        _gold.transform.parent = transform;
        Taken?.Invoke();
    }
}
