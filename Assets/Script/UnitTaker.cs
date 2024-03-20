using System;
using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    private UnitMover _unitMover;
    private Resource _gold;
    private Vector3 _currentGoldPosition;
    private bool _isBase = false;
    private bool _isGold = false;

    public event Action Taken;
    public event Action<Resource> Delivered;

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
        if (other.gameObject.TryGetComponent(out Resource gold))
        {
            if (gold.transform.position == _currentGoldPosition)
            {
                _isGold = true;
                _gold = gold;
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

    public void TakeGoldCord(Vector3 goldPosition)
    {
        _currentGoldPosition = goldPosition;
    }

    private void Interact()
    {
        //Debug.Log("IsInteract");

        if (_isBase && _gold != null)
        {
            //Debug.Log("isBase");
            Delivered?.Invoke(_gold);
            _gold = null;
        }
        else if (_isGold)
        {
            //Debug.Log("isGold");
            TakeGold();
        }
    }

    private void TakeGold()
    {
        _gold.transform.parent = transform;
        Taken?.Invoke();
    }
}
