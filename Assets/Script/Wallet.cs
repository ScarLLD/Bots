using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action ScoreChanged;

    public int ResourceCount { get; private set; }

    public void DecreaseResources(int price)
    {
        ResourceCount -= price;
        ScoreChanged?.Invoke();
    }

    public void IncreaseResource()
    {
        ResourceCount += 1;
        ScoreChanged?.Invoke();
    }
}
