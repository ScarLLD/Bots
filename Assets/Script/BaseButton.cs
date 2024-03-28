using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField] private UnitsBuyer _unitsBuyer;
    [SerializeField] private Button _actionButton;

    public event Action ButtonClicked;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
