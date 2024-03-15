using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UnitsTracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Unit[] _units;
    [SerializeField] private GoldPool _goldPool;
    [SerializeField] private GoldScanner _goldScanner;

    private Coroutine _grubCoroutine;
    private WaitForSeconds _wait;
    private bool _isGrubing;

    public float GetSpeed => _speed;

    public event Action<Vector3> Extract;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void OnEnable()
    {
        _goldScanner.Scanned += GrubGold;
    }

    private void OnDisable()
    {
        _goldScanner.Scanned -= GrubGold;
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.First(unit => unit.IsGrub == false);
        return unit != null;
    }

    public void ConfirmDelivery(Gold gold)
    {
        _goldPool.CollectGold(gold);
    }

    private void GrubGold()
    {
        if (_isGrubing == false)
            _grubCoroutine = StartCoroutine(Grub());
    }

    private IEnumerator Grub()
    {
        _isGrubing = true;

        while (_isGrubing)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_goldPool.TryGetAvailableGold(out Gold gold))
                {
                    gold.ChangeGrubStatus();
                    unit.ChangeGrubStatus();

                    Extract?.Invoke(gold.transform.position);
                }
            }

            yield return _wait;
        }

        StopCoroutine(_grubCoroutine);
    }
}
