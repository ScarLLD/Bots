using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Tracker _tracker;
    [SerializeField] private BaseButton _baseButton;

    private BaseCollector _baseCollector;
    private BaseBuilder _baseBuilder;

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();

        _tracker.Init(_baseCollector.GetResourcePool);
        _baseBuilder.Init(_baseButton);
    }
}
