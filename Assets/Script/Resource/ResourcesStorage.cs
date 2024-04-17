using System.Collections.Generic;
using UnityEngine;

public class ResourcesStorage : MonoBehaviour
{
    private List<Resource> _resources;

    private void Awake()
    {
        _resources = new List<Resource>();
    }

    public bool TryGetResource(out Resource resource)
    {
        resource = null;

        if (_resources.Count > 0)
        {
            int index = Random.Range(0, _resources.Count);
            resource = _resources[index];
            _resources.Remove(resource);
        }

        return resource != null;
    }

    public void TakeResource(Resource resource)
    {
        _resources.Add(resource);
    }
}
