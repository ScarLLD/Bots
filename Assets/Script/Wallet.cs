using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;

    private int _goldCount = 0;

    public int GetGoldCount => _goldCount;

    private void OnEnable()
    {
        _resourcePool.Collected += CollectResource;
    }

    private void OnDisable()
    {
        _resourcePool.Collected -= CollectResource;
    }

    private void CollectResource()
    {
        _goldCount += 1;
    }
}
