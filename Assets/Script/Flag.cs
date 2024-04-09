using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;

    private BaseCollector _baseCollector;
    private Unit _unit;

    public Transform GetStartPosition => _startPosition;

    private void Awake()
    {
        _baseCollector = transform.parent.GetComponent<BaseCollector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Unit unit))
            _unit = unit;
    }

    public void SpawnBase(Unit unit)
    {
        if (_unit == unit)
        {
            _baseCollector.GenerateBase(transform.position, _unit);

            Destroy(transform.gameObject);
        }        
    }
}