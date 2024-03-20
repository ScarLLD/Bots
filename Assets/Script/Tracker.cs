using System.Collections;
using System.Linq;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private Unit[] _units;
    [SerializeField] private ResourcePool _resourcePool;
    [SerializeField] private Scanner _scanner;

    private Coroutine _grubCoroutine;
    private WaitForSeconds _wait;
    private bool _isGrubing = false;

    public float GetSpeed => _speed;

    private void Awake()
    {
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
        unit = _units.FirstOrDefault(unit => unit.IsGrub == false);
        return unit != null;
    }

    public void ConfirmDelivery(Resource resource)
    {
        _resourcePool.CollectResource(resource);
    }

    private void GrubResources(int ResourceCount)
    {
        if (_isGrubing == false)
        {
            _grubCoroutine = StartCoroutine(Grub());
        }
    }

    private IEnumerator Grub()
    {
        _isGrubing = true;

        while (_isGrubing)
        {
            if (TryGetUnit(out Unit unit))
            {
                if (_resourcePool.TrySelectResource(out Resource resource))
                {
                    resource.ChangeGrubBool();    
                    unit.StartGrub(resource.transform.position);
                }
            }

            yield return _wait;
        }

        StopCoroutine(_grubCoroutine);
    }
}