using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitMover : MonoBehaviour
{
    private Unit _unit;
    private float _speed;
    private bool _isMove = false;

    public Transform StartTransfrom { get; private set; }

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _speed = transform.parent.GetComponent<Tracker>().Speed;
    }

    public void MoveToPoint(Transform target)
    {
        StartCoroutine(Move(target));
    }

    public void MoveBack()
    {
        MoveToPoint(StartTransfrom);
    }

    public void ChangeStartPosition(Transform tempTransform)
    {
        StartTransfrom = tempTransform;
    }

    private IEnumerator Move(Transform target)
    {
        _isMove = true;

        transform.LookAt(target);

        while (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                target.position, Time.deltaTime * _speed);

            if (transform.position == target.position)
                _isMove = false;

            yield return null;
        }

        _unit.Interact();
    }
}