using System;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ResourcePool _goldPool;

    private int GoldCount;

    public int GetResourceCount => GoldCount;

    public event Action Scanned;

    public void Scan()
    {
        GoldCount = _goldPool.GetResourceCount();

        Scanned?.Invoke();        
    }
}
