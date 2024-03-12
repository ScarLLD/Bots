using TMPro;
using UnityEngine;

public class GoldCountDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GoldScanner _scanner;

    private void OnEnable()
    {
        _scanner.Scanned += ShowGoldCount;
    }    

    private void OnDisable()
    {
        _scanner.Scanned -= ShowGoldCount;

    }

    private void ShowGoldCount()
    {
        _text.text = _scanner.GetGoldCount.ToString();
    }
}
