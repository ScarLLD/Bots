using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Employer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _minUnitsCount;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Shelter _shelter;

    private bool _isInterecting = false;
    private Wallet _wallet;
    private UnitSpawner _unitSpawner;
    private ResourcesStorage _resourcesStorage;
    private FlagStorage _flagStorage;
    private SheltersBuyer _shelterBuyer;
    private WaitForSeconds _wait;
    private List<Unit> _units;

    public event Action<Resource> ResourceDelivered;

    public float Speed => _speed;
    public int UnitsCount => _units.Count;

    private void Awake()
    {
        _unitSpawner = GetComponent<UnitSpawner>();

        _units = new List<Unit>();
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    public void Init(ResourcesStorage resourcesStorage, FlagStorage flagStorage, Wallet wallet)
    {
        _resourcesStorage = resourcesStorage;
        _flagStorage = flagStorage;
        _wallet = wallet;
    }

    public void SendUnitsWork()
    {
        StartCoroutine(Interect());
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);

        Debug.Log(_units.Where(unit => unit.IsBusy == false).ToList().Count());
        return unit != null;
    }

    public void ConfirmDelivery(Resource resource)
    {
        ResourceDelivered?.Invoke(resource);
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
                if (_flagStorage.TryTakeFlag(out Flag flag) && _units.Count > _minUnitsCount
                    && _shelterBuyer.GetShelterPrice > _wallet.ResourceCount)
                {
                    _unitSpawner.TakeSpawnPoint(unit.StartTransform);
                    _units.Remove(unit);

                    unit.ComeFlag(flag);
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