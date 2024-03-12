using System;
using UnityEngine;

public class GoldScanner : MonoBehaviour
{
    [SerializeField] private GoldPool _goldPool;

    private int GoldCount;

    public int GetGoldCount => GoldCount;

    public event Action Scanned;

    private void Start()
    {
        Scan();
    }

    public void Scan()
    {
        GoldCount = _goldPool.GetGoldCount;

        Scanned?.Invoke();

        if (GoldCount > 0)
            Debug.Log($"Найдено золото: {GoldCount}.");
        else
            Debug.Log($"Золота нет.");
    }
}
