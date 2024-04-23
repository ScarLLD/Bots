using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(UnitSpawner))]
public class Employer : MonoBehaviour
{
    [SerializeField] private int _minUnitsCount;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Shelter _shelter;

    private bool _isInterecting = false;
    private FlagStorage _flagStorage;
    private List<Unit> _units;
    private UnitSpawner _unitSpawner;
    private ResourcesStorage _resourcesStorage;
    private WaitForSeconds _wait;

    public event Action<Unit, Flag> UnitCameFlag;

    public int UnitsCount => _units.Count;

    private void Awake()
    {
        _unitSpawner = GetComponent<UnitSpawner>();

        _wait = new WaitForSeconds(_timeBetwenGrub);
        _units = new List<Unit>();
    }

    private void Start()
    {
        StartCoroutine(Interect());
    }

    public void Init(ResourcesStorage resourcesStorage)
    {
        _resourcesStorage = resourcesStorage;
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);

        return unit != null;
    }

    public void TakeUnit(Unit unit)
    {
        _units.Add(unit);
    }

    private IEnumerator Interect()
    {
        _isInterecting = true;

        while (_isInterecting)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_flagStorage._flag != null && _units.Count >= _minUnitsCount)
                // _sheltersBuyer.TryConfirmBuyPossibility())
                {
                    _unitSpawner.TakeSpawnPoint(unit.StartTransform);
                    _units.Remove(unit);

                    Debug.Log("ComeFlag");
                    unit.ComeFlag(_flagStorage._flag);
                    _flagStorage = null;
                }
                else if (_resourcesStorage.TryGetResource(out Resource resource))
                {
                    unit.StartGrub(resource);
                }
            }

            yield return _wait;
        }
    }
}