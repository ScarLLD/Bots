using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Button _actionButton;
    private BaseBuilder _baseBuilder;

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

    public void Init(BaseBuilder baseBuilder)
    {
        _baseBuilder = baseBuilder;
    }

    private void OnButtonClick()
    {
        //_baseBuilder.ShowTemplate(_base);
    }
}
