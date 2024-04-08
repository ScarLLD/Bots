using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;

    private List<Unit> _units;
    private Flag _flag;
    private ResourcePool _resourcePool;
    private WaitForSeconds _wait;
    private bool _isInterecting = false;

    public float GetSpeed => _speed;
    public int GetUnitsCount => _units.Count;

    private void Awake()
    {
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

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);
        return unit != null;
    }

    public void ConfirmDelivery(Resource resource)
    {
        _resourcePool.CollectResource(resource);
    }

    public void TakeUnit(Unit unit) => _units.Add(unit);

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
                if (_flag != null && _units.Count > 1)
                {
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