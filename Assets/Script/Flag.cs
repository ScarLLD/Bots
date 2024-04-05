using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;

    private BaseBuilder _baseBuilder;
    private Unit _unit;

    public Transform GetStartPosition => _startPosition;

    private void Awake()
    {
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();
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
            _baseBuilder.BuildBase(transform.position, unit);

            Destroy(transform.gameObject);
        }
    }
}