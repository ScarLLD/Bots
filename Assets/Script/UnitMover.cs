using System;
using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private UnitTaker _unitTaker;
    private Vector3 _startPosition;
    private Coroutine _moveCoroutine;
    private bool _isMove = false;
    private float _speed;

    public event Action Arrived;

    private void Awake()
    {
        _startPosition = transform.position;
        _unitTaker = GetComponent<UnitTaker>();
        _speed = transform.parent.GetComponent<UnitsTracker>().GetSpeed;
    }

    private void OnEnable()
    {
        _unitTaker.Taken += MoveBack;
    }
    
    private void OnDisable()
    {
        _unitTaker.Taken -= MoveBack;
    }

    private void MoveBack()
    {
        MoveToPoint(_startPosition);
    }

    public void MoveToPoint(Vector3 targetPosition)
    {
        _moveCoroutine = StartCoroutine(Move(targetPosition));
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        _isMove = true;

        while (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, Time.deltaTime * _speed);

            if (transform.position == targetPosition)
            {
                StopMove();
                Arrived?.Invoke();
            }

            yield return null;
        }
    }

    private void StopMove()
    {
        StopCoroutine(_moveCoroutine);
    }
}
