using System;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private Resource _targetResource;

    public event Action ResourceTaken;
    public event Action<Resource> ResourceDelivered;
    public event Action<Flag> FlagDeleted;

    public Flag TempFlag { get; private set; }
    public Shelter TempShelter { get; private set; }
    public Resource TempResource { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shelter shelter))
        {
            TempShelter = shelter;
        }
        else if (other.TryGetComponent(out Resource resource))
        {
            if (resource == _targetResource)
            {
                TempResource = resource;
            }
        }
        else if (other.TryGetComponent(out Flag flag))
        {
            TempFlag = flag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TempShelter = null;
    }

    public void ClearFlag()
    {
        FlagDeleted?.Invoke(TempFlag);

        TempFlag = null;
    }

    public void ChooseTarget(Resource resource)
    {
        _targetResource = resource;
    }

    public void TakeGold()
    {
        ResourceTaken?.Invoke();

        TempResource.transform.parent = transform;
        TempResource.transform.localPosition = _objectPosition;
    }

    public void PutGold()
    {
        ResourceDelivered?.Invoke(TempResource);

        TempResource = null;
        TempShelter = null;

        _targetResource = null;
    }

    public void Interact()
    {
        if (TempFlag != null)
        {
            ClearFlag();
        }
        else if (TempShelter != null)
        {
            PutGold();
        }
        else if (TempResource != null)
        {
            TakeGold();
        }
    }
}
