using TMPro;
using UnityEngine;

public class DisplayGoldInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _scannedCountText;
    [SerializeField] private TMP_Text _takenCountText;
    [SerializeField] private GoldScanner _scanner;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _scanner.Scanned += ShowScannedGoldCount;
        _wallet.Collected += ShowTakenGoldCount;
    }    

    private void OnDisable()
    {
        _scanner.Scanned -= ShowScannedGoldCount;
        _wallet.Collected -= ShowTakenGoldCount;
    }

    private void ShowScannedGoldCount()
    {
        _scannedCountText.text = _scanner.GetGoldCount.ToString();
    }

    private void ShowTakenGoldCount()
    {
        _takenCountText.text = _wallet.GetGoldCount.ToString();
    }
}
