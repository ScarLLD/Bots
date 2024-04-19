using System;
using UnityEngine;

public class UnitsBuyer : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private SheltersSpawner _sheltersSpawner;
    [SerializeField] private FlagsStorage _flagStorage;
    [SerializeField] private Wallet _wallet;

    public event Action<int> UnitSpawned;

    private void OnEnable()
    {
        _wallet.ScoreChanged += TryBuyUnit;
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= TryBuyUnit;
    }

    private void TryBuyUnit()
    {
        if (_flagStorage.Flags.Count == 0 && _wallet.ResourceCount >= _unitPrice)
        {

            if (_sheltersSpawner.TryChooseShelter(out Shelter shelter))
            {
                shelter.SpawnUnit();

                UnitSpawned?.Invoke(_unitPrice);
            }
        }
    }
}
