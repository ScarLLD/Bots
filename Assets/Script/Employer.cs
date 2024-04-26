using System;
using System.Collections;
using UnityEngine;

public class Employer : MonoBehaviour
{
    [SerializeField] private int _minUnitsCount;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private SheltersBuyer _sheltersBuyer;
    [SerializeField] private FlagStorage _flagStorage;
    [SerializeField] private UnitsStorage _unitsStorage;

    private bool _isInterecting;
    private ResourcesStorage _resourcesStorage;
    private WaitForSeconds _wait;

    public event Action<Unit> UnitCameFlag;


    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void Start()
    {
        StartCoroutine(Interect());
    }

    public void Init(ResourcesStorage resourcesStorage)
    {
        _resourcesStorage = resourcesStorage;
    }

    private IEnumerator Interect()
    {
        _isInterecting = true;

        while (_isInterecting)
        {
            if (_unitsStorage.TryGetUnit(out Unit unit))
            {
                if (_unitsStorage.UnitsCount >= _minUnitsCount
                    && _flagStorage.TryGetFlag(out Flag flag)
                    && _sheltersBuyer.TryConfrimBuyPossability())
                {
                    UnitCameFlag?.Invoke(unit);
                    _unitsStorage.RemoveUnit(unit);

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