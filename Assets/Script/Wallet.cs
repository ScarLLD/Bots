using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;

    public int GoldCount { get; private set; }

    public event Action ScoreChanged;

    private void OnEnable()
    {
        _resourcePool.ResourceCollected += CollectResource;
    }

    private void OnDisable()
    {
        _resourcePool.ResourceCollected -= CollectResource;
    }

    public void DecreaseResources(int price)
    {
        GoldCount -= price;

        ScoreChanged?.Invoke();
    }

    private void CollectResource()
    {
        GoldCount += 1;

        ScoreChanged?.Invoke();
    }
}
