using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;

    private ResourcePool _resourcePool;
    private List<Unit> _units;
    private WaitForSeconds _wait;
    private bool _isGrubing = false;

    public float GetSpeed => _speed;

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
        unit = _units.FirstOrDefault(unit => unit.GetTargetResource == null);
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

    public void SendUnit()
    {
        _units[Random.Range(0, _units.Count)].St
    }

    private IEnumerator Scan()
    {
        _isGrubing = true;

        while (_isGrubing)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_resourcePool.TrySelectResource(out Resource resource))
                {
                    unit.StartGrub(resource);
                }
            }

            yield return _wait;
        }
    }
}