using UnityEngine;

public class BaseCollector : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Vector3 _startPosition;

    public Vector3 GetStartPosition => _startPosition;
    public Wallet GetWallet => _wallet;
    public ResourcePool GetResourcePool => _resourcePool;

    private void Awake()
    {
        Instantiate(_basePrefab, _startPosition, Quaternion.identity, transform);
    }
}
