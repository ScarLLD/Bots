using System;
using UnityEngine;

public class GoldScanner : MonoBehaviour
{
    [SerializeField] private GoldPool _goldPool;

    private int GoldCount;

    public int GetGoldCount => GoldCount;

    public event Action Scanned;

    public void Scan()
    {
        GoldCount = _goldPool.GetGoldCount();

        Scanned?.Invoke();        
    }
}
