using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private List<BaseButton> _actionButtons;

    private void Awake()
    {
        _actionButtons = new List<BaseButton>();
    }

    public void Init(BaseButton button)
    {
        _actionButtons.Add(button);

        button.ButtonClicked += Interect;
    }

    private void Interect()
    {

    }

    private void OnDisable()
    {
        _actionButtons.Select(button => { button.ButtonClicked -= Interect; return button; });

        //ÇÀÄÀÂÀÒÜ İÒÓ ÁÀÇÓ ÊÀÆÄÎÉ ÊÍÎÏÊÅ À ÍÅ åâåíò
    }
}
