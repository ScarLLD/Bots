using System;
using UnityEngine;

public class SheltersBuyer : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private SheltersSpawner _sheltersSpawner;
    [SerializeField] private FlagsStorage _flagsStorage;
    [SerializeField] private Wallet _wallet;

    public event Action<int> ShelterSpawned;


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
        return _wallet.ResourceCount - _shelterPrice * _flagsStorage.Flags.Count >= 0;
    }

    private void BuyShelter()
    {
        ShelterSpawned?.Invoke(_shelterPrice);
    }
}
