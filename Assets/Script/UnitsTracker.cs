using System.Collections;
using System.Linq;
using UnityEngine;

public class UnitsTracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetwenGrub;
    [SerializeField] private UnitMover[] _units;
    [SerializeField] private GoldPool _goldPool;
    [SerializeField] private GoldScanner _goldScanner;

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
        _goldScanner.Scanned += GrubGold;
    }

    private void OnDisable()
    {
        _goldScanner.Scanned -= GrubGold;
    }

    public bool TryGetUnit(out UnitMover unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsWalk == false);
        return unit != null;
    }

    public void ConfirmDelivery(Gold gold)
    {
        _goldPool.CollectGold(gold);
    }

    private void GrubGold()
    {
        _grubCoroutine = StartCoroutine(Grub());
    }

    private IEnumerator Grub()
    {
        _isGrubing = true;

        while (_isGrubing)
        {

            if (TryGetUnit(out UnitMover unit))
            {
                if (_goldPool.TryGetAvailableGold(out Gold gold))
                {
                    unit.Grub(gold.transform.position);
                    gold.ChangeStatus();
                }                
            }
            

            yield return _wait;
        }

        StopCoroutine(_grubCoroutine);
    }
}
