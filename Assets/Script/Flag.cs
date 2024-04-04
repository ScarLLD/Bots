using UnityEngine;

public class Flag : MonoBehaviour
{
    private BaseBuilder _baseBuilder;
    private Unit _unit;

    private void Awake()
    {
        _baseBuilder = transform.parent.GetComponent<BaseBuilder>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Unit>(out Unit unit))
            _unit = unit;
    }

    public void ConfirmComing(Unit unit)
    {
        if (_unit == unit)
            _baseBuilder.BuildBase(transform.position, unit);
    }
}