using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Base _basePrefab;

    private BaseBuilder _baseBuilder;

    public Wallet GetWallet => _wallet;
    public ResourcePool GetResourcePool => _resourcePool;

    private void Awake()
    {
        _baseBuilder = GetComponent<BaseBuilder>();
    }

    private void Start()
    {
        Instantiate(_basePrefab, transform);
    }

    private void OnEnable()
    {
        _baseBuilder.FlagInstalled += SendRequest;
    }

    private void OnDisable()
    {
        _baseBuilder.FlagInstalled -= SendRequest;
    }

    private void SendRequest(Base tempBase, Flag flag)
    {
        tempBase.WaitRequire(flag);
    }
}
