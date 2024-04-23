using UnityEngine;

public class Shelter : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private Employer _employer;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Wallet _wallet;

    private ResourceSpawner _resourceSpawner;
    private SheltersSpawner _sheltersSpawner;

    public Flag Flag { get; private set; }
    public UnitSpawner UnitSpawner => _unitSpawner;
    public int UnitsCount => _employer.UnitsCount;

    private void OnEnable()
    {
        _employer.UnitCameFlag += SendBuildRequest;
    }

    private void OnDisable()
    {
        _employer.UnitCameFlag -= SendBuildRequest;
    }

    public void TakeGold(Resource resource)
    {
        _resourceSpawner.TakeResource(resource);
        _wallet.IncreaseResource();
    }

    public void Init(ResourceSpawner resourceSpawner, ResourcesStorage resourcesStorage,
        SheltersSpawner sheltersSpawner, Camera camera)
    {
        _resourceSpawner = resourceSpawner;
        _sheltersSpawner = sheltersSpawner;

        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = camera;
        _canvas.renderMode = RenderMode.WorldSpace;

        _employer.Init(resourcesStorage);
        _flagSetter.Init(camera);
    }

    private void SendBuildRequest(Unit unit, Flag flag)
    {
        _sheltersSpawner.BuildShelter(unit, flag);
    }

    public void TakeUnit(Unit unit)
    {
        _unitSpawner.TakeUnit(unit);
    }
}
