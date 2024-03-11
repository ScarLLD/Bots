using System;
using UnityEngine;

public class GoldScanner : MonoBehaviour
{
    [SerializeField] private GoldPool _goldPool;

    public int GoldCount;

    public event Action Scanned;

    public void Scan()
    {
        GoldCount = _goldPool.GetGoldCount;

        if (GoldCount > 0)
            Debug.Log($"������� ������: {GoldCount}.");
        else
            Debug.Log($"������ ���.");
    }
}
