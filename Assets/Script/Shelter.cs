using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class Shelter : MonoBehaviour
{
    [SerializeField] private FlagStorage _flagStorage;
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private UnitsStorage _unitsStorage;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private Employer _employer;
    [SerializeField] private Canvas _canvas;

    private Wallet _wallet;
    private ResourcePool _resourcePool;
    private SheltersSpawner _sheltersSpawner;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
    }

    public void Init(ResourcesStorage resourcesStorage, ResourcePool resourcePool,
        SheltersSpawner sheltersSpawner, Camera camera)
    {
        _resourcePool = resourcePool;
        _sheltersSpawner = sheltersSpawner;

        _employer.Init(resourcesStorage);
        _flagSetter.Init(camera);

        NormalizeCanvas(camera);
    }

    public void TakeGold(Resource resource)
    {
        _resourcePool.PutResource(resource);
        _wallet.IncreaseResource();
    }

    public void SendBuildRequest(Unit unit, Flag flag)
    {
        _sheltersSpawner.BuildShelter(unit, flag);
        _flagStorage.RemoveFlag();
        Destroy(flag);
    }

    public void TakeUnit(Unit unit)
    {
        unit.Init(_unitSpawner.GetSpawnPoint, this);
        _unitsStorage.TakeUnit(unit);
    }

    private void NormalizeCanvas(Camera camera)
    {
        _canvas.worldCamera = camera;
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.transform.position = transform.position;
    }
}
