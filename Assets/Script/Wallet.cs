using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private GoldPool _goldPool;

    private int _goldCount;

    public event Action Collected;

    public int GetGoldCount => _goldCount;

    private void OnEnable()
    {
        _goldPool.Collected += CollectGold;
    }

    private void OnDisable()
    {
        _goldPool.Collected -= CollectGold;
    }

    private void CollectGold()
    {
        _goldCount++;
        Collected?.Invoke();
    }    
}
