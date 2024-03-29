using UnityEngine;
using UnityEngine.UI;

public class UnitsBuyer : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _actionButton;
    [SerializeField] private UnitSpawner _unitSpawner;

    private Wallet _wallet;

    public bool IsFull { get; private set; } = false;
    public Button GetButton => _actionButton;

    private void OnEnable()
    {
        _wallet.ScoreChanged += ShowInteractable;
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= ShowInteractable;
        _actionButton.onClick.RemoveListener(OnButtonClick);

    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void BuyUnit()
    {
        _wallet.DecreaseResources(_unitPrice);
    }

    public void ShowInteractable()
    {
        if (_wallet.GoldCount >= _unitPrice)
            _actionButton.interactable = true;
        else
            _actionButton.interactable = false;

    }

    public void ShowButton()
    {
        if (IsFull == false)
            _canvasGroup.alpha = 1;
        else
            _canvasGroup.alpha = 0;
    }

    private void OnButtonClick()
    {
        IsFull = _unitSpawner.TrySpawnUnit();

        if (IsFull == false)
            BuyUnit();
        else
            _canvasGroup.alpha = 0;
    }
}
