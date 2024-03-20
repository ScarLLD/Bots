using System;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;

    private int ResourceCount;

    public event Action<int> Scanned;

    public void Scan()
    {
        ResourceCount = _resourcePool.PooledObject.Count();

        Scanned?.Invoke(ResourceCount);        
    }
}
