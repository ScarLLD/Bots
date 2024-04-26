using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitsStorage : MonoBehaviour
{
    private List<Unit> _units;

    public int UnitsCount => _units.Count;

    private void Awake()
    {
        _units = new List<Unit>();
    }

    public bool TryGetUnit(out Unit unit)
    {
        unit = _units.FirstOrDefault(unit => unit.IsBusy == false);

        return unit != null;
    }

    public void TakeUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }
}
