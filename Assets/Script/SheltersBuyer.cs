using System;
using UnityEngine;

public class SheltersBuyer : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private SheltersSpawner _sheltersSpawner;
    [SerializeField] private FlagStorage _flagsStorage;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _sheltersSpawner.ShelterBuilded += BuyShelter;
    }

    private void OnDisable()
    {
        _sheltersSpawner.ShelterBuilded -= BuyShelter;
    }

    public bool TryConfirmBuyPossibility()
    {
        return _wallet.ResourceCount - _shelterPrice >= 0;
    }

    private void BuyShelter()
    {
        _wallet.DecreaseResources(_shelterPrice);
    }
}
