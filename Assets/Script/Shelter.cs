using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;

    private ResourceSpawner _resourceSpawner;
    private SheltersSpawner _sheltersSpawner;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;

    private void OnEnable()
    {
        _employer.ResourceDelivered += TransferResource;
        _employer.UnitCameFlag += SendBuildRequest;
    }

    private void OnDisable()
    {
        _employer.ResourceDelivered -= TransferResource;
        _employer.UnitCameFlag -= SendBuildRequest;

    }

    public void Init(ResourceSpawner resourceSpawner, ResourcesStorage resourcesStorage,
        SheltersBuyer sheltersBuyer, SheltersSpawner sheltersSpawner)
    {
        _resourceSpawner = resourceSpawner;
        _sheltersSpawner = sheltersSpawner;

        _employer.Init(resourcesStorage, sheltersBuyer);
    }

    private void TransferResource(Resource resource)
    {
        _resourceSpawner.CollectResource(resource);
    }

    private void SendBuildRequest(Unit unit, Flag flag)
    {
        _sheltersSpawner.BuildShelter(unit, flag);
    }

    public void GiveBuildTask(Flag flag)
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
