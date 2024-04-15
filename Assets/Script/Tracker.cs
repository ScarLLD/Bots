using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _minUnitsCount;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Shelter _shelter;

    private bool _isInterecting = false;
    private UnitSpawner _unitSpawner;
    private ResourceSpawner _resourceSpawner;
    private WaitForSeconds _wait;
    private List<Unit> _units;
    private Flag _flag;

    public float Speed => _speed;
    public int UnitsCount => _units.Count;

    private void Awake()
    {
        _unitSpawner = GetComponent<UnitSpawner>();

        _units = new List<Unit>();
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void Start()
    {
        StartCoroutine(Interect());
    }

    public void Init(ResourceSpawner resourceSpawner)
    {
        _resourceSpawner = resourceSpawner;
    }

    public void SendBuildRequest(Flag flag, Unit unit)
    {
        _shelter.BuildBase(flag, unit);
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);
        return unit != null;
    }

    public void ConfirmDelivery(Resource resource)
    {
        _resourceSpawner.CollectResource(resource);
    }

    public void TakeUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void TakeFlag(Flag flag)
    {
        _flag = flag;
    }

    private IEnumerator Interect()
    {
        _isInterecting = true;

        while (_isInterecting)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_flag != null && _units.Count > _minUnitsCount && _shelter.TryBuyShelter)
                {
                    _unitSpawner.TakeSpawnPoint(unit.StartTransform);
                    _units.Remove(unit);

                    unit.ComeFlag(_flag);
                    _flag = null;
                }
                else if (_resourceSpawner.TrySelectResource(out Resource resource))
                {
                    unit.StartGrub(resource);
                }
            }

            yield return _wait;
        }
    }
}