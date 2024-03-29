using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Scanner _scanner;

    private List<Unit> _units;
    private WaitForSeconds _wait;
    private bool _isGrubing = false;

    public float GetSpeed => _speed;

    private void Awake()
    {      
        _units = new List<Unit>();
        _wait = new WaitForSeconds(_timeBetwenGrub);
    }

    private void OnEnable()
    {
        _scanner.Scanned += GrubResources;
    }

    private void OnDisable()
    {
        _scanner.Scanned -= GrubResources;
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

    private void GrubResources(int ResourceCount)
    {
        if (_isGrubing == false && ResourceCount > 0)
        {
            StartCoroutine(Grub());
        }
    }

    private IEnumerator Grub()
    {
        _isGrubing = true;

        while (_isGrubing)
        {
            if (TryGetUnit(out Unit unit))
                if (_resourcePool.TrySelectResource(out Resource resource))
                    unit.StartGrub(resource);

            yield return _wait;
        }
    }
}