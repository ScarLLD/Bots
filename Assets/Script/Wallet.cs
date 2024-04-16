using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;

    public event Action ScoreChanged;

    public int GoldCount { get; private set; }

    private void OnEnable()
    {
        _resourceSpawner.ResourceCollected += CollectResource;
    }

    private void OnDisable()
    {
        _resourceSpawner.ResourceCollected -= CollectResource;
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
