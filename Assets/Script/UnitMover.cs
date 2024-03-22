using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitTaker))]
public class UnitMover : MonoBehaviour
{
    private UnitTaker _unitTaker;
    private Vector3 _startPosition;
    private bool _isMove = false;
    private float _speed;

    public event Action Arrived;

    private void Awake()
    {
        _startPosition = transform.position;
        _unitTaker = GetComponent<UnitTaker>();
        _speed = transform.parent.GetComponent<Tracker>().GetSpeed;
    }

    private void OnEnable()
    {
        _unitTaker.Taken += MoveBack;
    }

    private void OnDisable()
    {
        _unitTaker.Taken -= MoveBack;
    }

    public void MoveToPoint(Vector3 targetPosition)
    {
        StartCoroutine(Move(targetPosition));
    }

    private void MoveBack()
    {
        MoveToPoint(_startPosition);
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        _isMove = true;

        transform.LookAt(targetPosition);

        while (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                targetPosition, Time.deltaTime * _speed);

            if (transform.position == targetPosition)            
                _isMove = false; 

            yield return null;
        }

        Arrived?.Invoke();
    }
}