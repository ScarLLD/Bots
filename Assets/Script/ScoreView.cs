using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _takenGoldCount;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _takenGoldCount.text = _wallet.ResourceCount.ToString();
    }
}
