using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField] private UnitsBuyer _unitsBuyer;

    private Button _actionButton;
    private BaseBuilder _baseBuilder;
    private BuyUnitButtonView _unitButtonView;

    private void Awake()
    {
        _actionButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Init(BaseBuilder baseBuilder, BuyUnitButtonView buyUnitButtonView)
    {
        _baseBuilder = baseBuilder;
        _unitButtonView = buyUnitButtonView;
    }

    private void OnButtonClick()
    {
        _unitButtonView.ShowButton(_unitsBuyer);
        _baseBuilder.Interect();
    }
}
