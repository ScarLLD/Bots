using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Unit[] _units;
    [SerializeField] private ResourcePool _goldPool;
    [SerializeField] private Scanner _goldScanner;

    private Coroutine _grubCoroutine;
    private WaitForSeconds _wait;
    private bool _isGrubing;

    public float GetSpeed => _speed;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void OnEnable()
    {
        _goldScanner.Scanned += GrubResources;
    }

    private void OnDisable()
    {
        _goldScanner.Scanned -= GrubResources;
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsGrub == false);
        return unit != null;
    }

    public void ConfirmDelivery(Resource gold)
    {
        _goldPool.CollectGold(gold);
    }

    private void GrubResources()
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
                if (_goldPool.TryGetNotGrubResource(out Resource gold))
                {
                    gold.ChangeGrubStatus();
                    unit.ChangeGrubStatus();

                    unit.StartGrub(gold.transform.position);
                }
            }

            yield return _wait;
        }

        StopCoroutine(_grubCoroutine);
    }
}
