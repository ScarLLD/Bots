using System.Linq;
using UnityEngine;

public class UnitsTracker : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private UnitMover[] _units;
    [SerializeField] private GoldPool _goldPool;

    private UnitMover unit;
    public float GetSpeed => _speed;

    public void Grub()
    {
        if (TryGetUnit())
        {
            if (_goldPool.TryGetAvailableGold(out Gold gold))
            {
                unit.Grub(gold);
            }
        }
    }

    public bool TryGetUnit()
    {
        unit = null;
        unit = _units.FirstOrDefault(unit => unit.IsWalk == false);
        return unit != null;
    }

    public void SetContainerParent(Gold gold)
    {
        _goldPool.SetContainerParent(gold);
    }

    public void SetUnitParent(Gold gold)
    {
        gold.transform.parent = unit.transform;
    }
}
