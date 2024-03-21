using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour
{
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Button _actionButton;

    private int ResourceCount;

    public event Action<int> Scanned;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Scan();
    }

    public void Scan()
    {
        ResourceCount = _resourcePool.GetActiveResources().Count();

        Scanned?.Invoke(ResourceCount);        
    }
}
