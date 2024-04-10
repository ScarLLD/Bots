using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitTaker))]
public class UnitMover : MonoBehaviour
{
    public Transform StartTransfrom { get; private set; }
    private UnitTaker _unitTaker;
    private float _speed;
    private bool _isMove = false;

    private void Awake()
    {
        _unitTaker = GetComponent<UnitTaker>();
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

        _unitTaker.Interact();
    }
}