using System;
using UnityEngine;

public class SheltersBuyer : MonoBehaviour
{
    [SerializeField] private int _shelterPrice;
    [SerializeField] private SheltersSpawner _sheltersSpawner;
    [SerializeField] private FlagsStorage _flagsStorage;
    [SerializeField] private Wallet _wallet;

    public event Action<int> ShelterSpawned;

    public int GetShelterPrice => _shelterPrice;

    private void OnEnable()
    {
        //_wallet.ScoreChanged += BuyShelter;
        _sheltersSpawner.ShelterBuilded += BuyShelter;
    }

    private void OnDisable()
    {
        //_wallet.ScoreChanged -= BuyShelter;
        _sheltersSpawner.ShelterBuilded -= BuyShelter;
    }

    public bool TryConfirmBuyPossibility()
    {
        return _wallet.ResourceCount - _shelterPrice * _flagsStorage.Flags.Count >= _shelterPrice;
    }

    private void BuyShelter()
    {
        ShelterSpawned?.Invoke(_shelterPrice);
    }
}
