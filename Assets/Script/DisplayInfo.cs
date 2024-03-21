using TMPro;
using UnityEngine;

public class DisplayInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _takenGoldCount;
    [SerializeField] private Wallet _wallet;

    private void Awake()
    {
        ShowTakenResourceCount();
    }

    private void OnEnable()
    {
        _wallet.Changed += ShowTakenResourceCount;
    }

    private void OnDisable()
    {
        _wallet.Changed -= ShowTakenResourceCount;
    }

    private void ShowTakenResourceCount()
    {
        _takenGoldCount.text = _wallet.GetGoldCount.ToString();
    }
}
