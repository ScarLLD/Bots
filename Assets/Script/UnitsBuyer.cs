using UnityEngine;

public class UnitsBuyer : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private FlagStorage _flagStorage;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private Wallet _wallet;

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
        if (_flagStorage.TryGetFlag(out Flag flag) == false
            && _spawner.GetSpawnPointsCount > 0
            && _wallet.ResourceCount >= _unitPrice)
        {
            _spawner.SpawnUnit();
            _wallet.DecreaseResources(_unitPrice);
        }
    }
}
