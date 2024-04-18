using System;
using UnityEngine;

public class SheltersBuyer : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;

    private SheltersSpawner _shelterSpawner;

    public event Action<int> ShelterBought;

    public int GetShelterPrice => _shelterPrice;

    private void OnEnable()
    {
        _shelterSpawner.ShelterBuilded += BuyShelter;
    }

    private void OnDisable()
    {
        _shelterSpawner.ShelterBuilded -= BuyShelter;
    }

    private void BuyShelter()
    {
        ShelterBought?.Invoke(_shelterPrice);
    }
}
