using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Base _basePrefab;

    public Wallet GetWallet => _wallet;
    public ResourcePool GetResourcePool => _resourcePool;

    private void Awake()
    {
        Instantiate(_basePrefab, transform);
    }
}
