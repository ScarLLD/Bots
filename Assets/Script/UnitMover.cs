using System.Collections;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    private UnitsTracker _unitsTracker;
    private float _speed;
    private Vector3 _startPosition;
    private Coroutine _moveCoroutine;
    private bool _isWalking;
    private bool _isGrubing;

    public bool IsWalk => _isWalking;

    private void Awake()
    {
        Init();
        _startPosition = transform.position;
    }

    private void Init()
    {
        _unitsTracker = transform.parent.gameObject.GetComponent<UnitsTracker>();
        _speed = _unitsTracker.GetSpeed;
    }

    public void Grub(Gold gold)
    {
        gold.ChangeStatus();
        _moveCoroutine = StartCoroutine(Move(gold));
    }

    private IEnumerator Move(Gold gold)
    {
        Vector3 targetPosition = gold.transform.position;

        _isGrubing = true;

        while (_isGrubing)
        {
            _isWalking = true;

            while (_isWalking)
            {
                Debug.Log("isWalk");

                transform.position = Vector3.MoveTowards(transform.position,
                    targetPosition, _speed * Time.deltaTime);

                if (transform.position == targetPosition)
                {
                    if (gold.transform.position == transform.position)
                    {
                        
                    }

                    _isWalking = false;
                }

                yield return null;
            }

            if (transform.position == _startPosition)
                _isGrubing = false;
            else
                targetPosition = _startPosition;
        }

        StopCoroutine(_moveCoroutine);
    }
}
