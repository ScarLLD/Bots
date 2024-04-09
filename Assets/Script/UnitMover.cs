using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitTaker))]
public class UnitMover : MonoBehaviour
{
    private Transform _startTransfrom;
    private UnitTaker _unitTaker;
    private bool _isMove = false;
    private float _speed;

    public Transform GetStartTransfrom => _startTransfrom;

    private void Awake()
    {
        _unitTaker = GetComponent<UnitTaker>();
        _speed = transform.parent.GetComponent<Tracker>().GetSpeed;
    }

    public void MoveToPoint(Transform target)
    {
        StartCoroutine(Move(target));
    }

    public void MoveBack()
    {
        MoveToPoint(_startTransfrom);
    }

    public void ChangeStartPosition(Transform tempTransform)
    {
        _startTransfrom = tempTransform;
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

        _unitTaker.Interact();
    }
}