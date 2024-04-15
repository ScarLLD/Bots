using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _goldPrefab;

    public Queue<Resource> Pool { get; private set; }
    public Transform Container => _container;

    private void Awake()
    {
        Pool = new Queue<Resource>();
    }

    public Resource GetResource()
    {
        if (Pool.Count == 0)
        {
            var resource = Instantiate(_goldPrefab, transform.position, transform.rotation, _container);

            return resource;
        }

        return Pool.Dequeue();
    }

    public void PutResource(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.parent = _container;
        Pool.Enqueue(resource);
    }
}
