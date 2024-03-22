using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;

    private int _goldCount = 0;

    public int GetGoldCount => _goldCount;

    public event Action ScoreChanged;

    private void OnEnable()
    {
        _resourcePool.ResourceCollected += CollectResource;
    }

    private void OnDisable()
    {
        _resourcePool.ResourceCollected -= CollectResource;
    }

    private void CollectResource()
    {
        _goldCount += 1;

        ScoreChanged?.Invoke();
    }
}
