using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitMover : MonoBehaviour
{
    private float _speed;
    private bool _isMove = false;

    public event Action Arrived;

    public Transform StartTransfrom { get; private set; }

    public void Init(int speed)
    {
        _speed = speed;
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

        Arrived?.Invoke();
    }
}