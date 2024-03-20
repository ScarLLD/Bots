using TMPro;
using UnityEngine;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _takenGoldCount;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Wallet _wallet;

    private void Awake()
    {
        ShowTakenResourceCount();
    }

    private void OnEnable()
    {
        _resourcePool.Collected += ShowTakenResourceCount;
    }

    private void OnDisable()
    {
        _resourcePool.Collected -= ShowTakenResourceCount;
    }

    private void ShowTakenResourceCount()
    {
        _takenGoldCount.text = _wallet.GetGoldCount.ToString();
    }
}
