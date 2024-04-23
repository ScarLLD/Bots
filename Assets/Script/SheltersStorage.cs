using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheltersStorage : MonoBehaviour
{
    private List<Shelter> _shelters;

    private void Awake()
    {
        _shelters = new List<Shelter>();
    }

    public void PutShelter(Shelter shelter)
    {
        _shelters.Add(shelter);
    }

    public bool TryChooseShelter(out Shelter shelter)
    {
        shelter = _shelters.FirstOrDefault(shelter => shelter.UnitSpawner.GetSpawnPointsCount > 0);

        return shelter != null;
    }
}
