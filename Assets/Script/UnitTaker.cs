using System;
using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    private UnitsTracker _unitsTracker;
    private Gold _gold;

    public event Action Taken;
    public event Action Delivered;

    private void Awake()
    {
        _unitsTracker = transform.parent.gameObject.GetComponent<UnitsTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Gold>(out Gold gold))
        {
            Debug.Log("gold taken");
            _gold = gold;
            _gold.transform.parent = transform;
            Taken?.Invoke();
        }
        else if (other.gameObject.GetComponent<CollectingZone>() && _gold != null)
        {
            _unitsTracker.ConfirmDelivery(_gold);
            _gold = null;
            Delivered?.Invoke();
        }
    }    
}
