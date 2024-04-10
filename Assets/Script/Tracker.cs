using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;

    private UnitSpawner _unitSpawner;
    private ResourcePool _resourcePool;
    private WaitForSeconds _wait;
    private List<Unit> _units;
    private Shelter _base;
    private Flag _flag;
    private bool _isInterecting = false;

    public float Speed => _speed;
    public int UnitsCount => _units.Count;

    private void Awake()
    {
        _unitSpawner = GetComponent<UnitSpawner>();
        _base = transform.parent.GetComponent<Shelter>();

        _units = new List<Unit>();
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void Start()
    {
        StartCoroutine(Scan());
    }

    public void Init(ResourcePool resourcePool)
    {
        _resourcePool = resourcePool;
    }

    public void BuildBase(Flag flag, Unit unit)
    {
        _base.BuildBase(flag, unit);
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);
        return unit != null;
    }

    public void ConfirmDelivery(Resource resource)
    {
        _resourcePool.CollectResource(resource);
    }

    public void TakeUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void TakeFlag(Flag flag)
    {
        _flag = flag;
    }

    private IEnumerator Scan()
    {
        _isInterecting = true;

        while (_isInterecting)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_flag != null && _units.Count > 1 && _base.TryBuyBase)
                {
                    _unitSpawner.TakeSpawnPoint(unit.StartTransform);
                    _units.Remove(unit);

                    unit.ComeFlag(_flag);
                    _flag = null;
                }
                else if (_resourcePool.TrySelectResource(out Resource resource))
                {
                    unit.StartGrub(resource);
                }
            }

            yield return _wait;
        }
    }
}