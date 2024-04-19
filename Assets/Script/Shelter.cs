using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;

    private ResourceSpawner _resourceSpawner;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;

    private void OnEnable()
    {
        _employer.ResourceDelivered += TransferResource;
    }

    private void OnDisable()
    {
        _employer.ResourceDelivered -= TransferResource;
    }

    public void Init(ResourceSpawner resourceSpawner, 
        ResourcesStorage resourcesStorage, SheltersBuyer sheltersBuyer)
    {
        _resourceSpawner = resourceSpawner;

        _employer.Init(resourcesStorage, sheltersBuyer);
    }

    private void TransferResource(Resource resource)
    {
        _resourceSpawner.CollectResource(resource);
    }

    public void SendBuildRequest(Flag flag)
    {
        _employer.TakeFlag(flag);

        Flag = flag;
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }

    public void SpawnUnit()
    {
        _unitSpawner.SpawnUnit();
    }
}
