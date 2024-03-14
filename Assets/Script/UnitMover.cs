using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private UnitsTracker _unitsTracker;
    private UnitTaker _unitTaker;
    private float _speed;
    private Vector3 _startPosition;
    private Coroutine _moveCoroutine;
    private bool _isWalking;

    public bool IsWalk => _isWalking;

    private void Awake()
    {
        _unitsTracker = transform.parent.gameObject.GetComponent<UnitsTracker>();
        _unitTaker = GetComponent<UnitTaker>();
        _speed = _unitsTracker.GetSpeed;
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        _unitTaker.Taken += MoveBack;
    }

    private void OnDisable()
    {
        _unitTaker.Taken -= MoveBack;
    }

    public void Grub(Vector3 goldPosition)
    {        
        MoveGold(goldPosition);
    }        

    private void MoveGold(Vector3 goldPosition)
    {
        _moveCoroutine = StartCoroutine(Move(goldPosition));
    }

    private void MoveBack()
    {
        StopCoroutine(_moveCoroutine);
        _moveCoroutine = StartCoroutine(Move(_startPosition));
    }

    private IEnumerator Move(Vector3 targetPosition)
    {        
            _isWalking = true;

            while (_isWalking)
            {   
                transform.position = Vector3.MoveTowards(transform.position,
                    targetPosition, _speed * Time.deltaTime);

                if (transform.position == targetPosition)                
                    _isWalking = false;                

                yield return null;
            } 

        StopCoroutine(_moveCoroutine);

        Debug.Log("stoped");
    }
}
