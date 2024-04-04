using UnityEngine;
using UnityEngine.UI;

public class BuyUnitButtonView : MonoBehaviour
{
    private Button _actionButton;

    public void ShowButton(UnitsAdder unitsBuyer)
    {
        if (_actionButton != null)
            _actionButton.interactable = false;

        _actionButton = unitsBuyer.GetButton;

        unitsBuyer.ShowButton();
    }
}
