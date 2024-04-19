using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private UnitsBuyer _unitsBuyer;
    [SerializeField] private SheltersBuyer _sheltersBuyer;

    public event Action ScoreChanged;

    public int ResourceCount { get; private set; }

    private void OnEnable()
    {
        _resourceSpawner.ResourceCollected += CollectResource;
        _sheltersBuyer.ShelterSpawned += DecreaseResources;
        _unitsBuyer.UnitSpawned += DecreaseResources;
    }

    private void OnDisable()
    {
        _resourceSpawner.ResourceCollected -= CollectResource;
        _sheltersBuyer.ShelterSpawned -= DecreaseResources;
        _unitsBuyer.UnitSpawned -= DecreaseResources;
    }

    public void DecreaseResources(int price)
    {
        ResourceCount -= price;
        ScoreChanged?.Invoke();
    }

    private void CollectResource()
    {
        ResourceCount += 1;
        ScoreChanged?.Invoke();
    }
}
