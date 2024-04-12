using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitTaker : MonoBehaviour
{
    [SerializeField] private Vector3 _objectPosition;

    private Unit _unit;
    private Resource _targetResource;

    public Flag TempFlag { get; private set; }
    public Shelter TempShelter { get; private set; }
    public Resource TempResource { get; private set; }

    private void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Shelter shelter))
        {
            TempShelter = shelter;
        }
        else if (other.TryGetComponent(out Resource gold))
        {
            if (gold == _targetResource)
            {
                TempResource = gold;
            }
        }
        else if (other.TryGetComponent<Flag>(out Flag flag))
        {
            TempFlag = flag;
        }
    }

    public void Init(Shelter shelter)
    {
        TempShelter = shelter;
    }

    public void ClearFlag()
    {
        TempFlag = null;
    }

    public void ChooseTarget(Resource resource)
    {
        resource.ChangeGrubBool();
        _targetResource = resource;
    }

    public void TakeGold()
    {
        TempResource.transform.parent = transform;
        TempResource.transform.localPosition = _objectPosition;
    }

    public void PutGold()
    {
        _unit.ConfirmDelivery(TempResource);

        TempResource = null;
        TempShelter = null;

        _targetResource = null;
    }
}
