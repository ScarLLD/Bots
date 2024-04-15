using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;

    public int GoldCount { get; private set; }

    public event Action ScoreChanged;

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
