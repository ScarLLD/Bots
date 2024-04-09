using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _takenGoldCount;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.ScoreIncreased += OnScoreChanged;
    }

    private void OnDisable()
    {
        _wallet.ScoreIncreased -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _takenGoldCount.text = _wallet.GoldCount.ToString();
    }
}
