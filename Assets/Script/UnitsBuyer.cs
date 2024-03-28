using UnityEngine;
using UnityEngine.UI;

public class UnitsBuyer : MonoBehaviour
{
    [SerializeField] private int _unitPrice;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _actionButton;
    [SerializeField] private UnitSpawner _unitSpawner;

    private BaseCollector _baseCollector;
    private Wallet _wallet;

    public bool IsFull { get; private set; } = false;

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();

        _wallet = _baseCollector.GetWallet;
    }

    private void OnEnable()
    {
        _wallet.ScoreChanged += ShowButton;
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _wallet.ScoreChanged -= ShowButton;
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

    private void ShowButton()
    {
        if (_canvasGroup.alpha > 0 && _wallet.GoldCount >= _unitPrice)
            _actionButton.interactable = true;
        else
            _actionButton.interactable = false;
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
