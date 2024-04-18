using System;
using UnityEngine;

public class UnitsBuyer : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private SheltersSpawner _sheltersCollector;
    [SerializeField] private FlagStorage _flagStorage;

    public event Action<int> UnitBought;

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
            if (_sheltersCollector.TryChooseShelter(out Shelter shelter))
            {
                shelter.BuyUnit();

                UnitBought?.Invoke(_unitPrice);
            }
        }
    }
}
