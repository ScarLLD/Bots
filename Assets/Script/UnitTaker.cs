using UnityEngine;

public class UnitTaker : MonoBehaviour
{
    private UnitsTracker _unitsMover;
    private Gold _tempGold;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _unitsMover = transform.parent.gameObject.GetComponent<UnitsTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Gold>(out Gold gold))
        {
            _tempGold = gold;
            _unitsMover.SetUnitParent(gold);
        }
        else if(other.gameObject.GetComponent<CollectingZone>() && _tempGold != null)
        {
            _unitsMover.SetContainerParent(_tempGold);
            _tempGold = null;
        }
    }
}
