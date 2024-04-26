using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private ParticlePool _particlePool;

    private Queue<Resource> _pool;

    private void Awake()
    {
        _pool = new Queue<Resource>();
    }

    public Resource GetResource()
    {
        if (_pool.Count == 0)
        {
            return Instantiate(_resourcePrefab, transform.position, transform.rotation, _container);
        }

        return _pool.Dequeue();
    }

    public void PutResource(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.parent = _container;
        _pool.Enqueue(resource);

        ParticleSystem particle = _particlePool.GetParticle();
        particle.transform.position = resource.transform.position;
        particle.gameObject.SetActive(true);
    }
}
