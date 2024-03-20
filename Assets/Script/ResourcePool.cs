using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _goldPrefab;
    [SerializeField] private ParticlePool _particlePool;

    private Queue<Resource> _pool;

    public IEnumerable<Resource> PooledObject => _pool;

    public event Action Collected;

    private void Awake()
    {
        _pool = new Queue<Resource>();
    }

    public Resource GetResource()
    {
        if (_pool.Count == 0)
        {
            var resource = Instantiate(_goldPrefab, transform.position, transform.rotation);
            resource.transform.parent = _container;


            return resource;
        }

        return _pool.Dequeue();
    }

    public void PutResource(Resource resource)
    {
        resource.gameObject.SetActive(false);
        resource.transform.parent = _container;
        _pool.Enqueue(resource);
    }

    public void CollectResource(Resource resource)
    {
        PutResource(resource);
        resource.ChangeGrubBool();

        SpawnParticle(resource.transform.position);

        Collected?.Invoke();
    }

    private void SpawnParticle(Vector3 spawnPosition)
    {
        ParticleSystem particle = _particlePool.GetParticle();

        particle.transform.position = spawnPosition;
        particle.gameObject.SetActive(true);
    }

    public bool TrySpawn(Vector3 resourcePosition)
    {
        bool isEmpty = true;

        isEmpty = GetActiveResources().Any(resource => resource.transform.position == resourcePosition);

        return !isEmpty;
    }

    public bool TrySelectResource(out Resource resource)
    {
        resource = GetActiveResources().FirstOrDefault(resource => resource.IsGrub == false);
        return resource != null;
    }

    private IEnumerable<Resource> GetActiveResources()
    {
        return GetAllResources().Where(resource => resource.gameObject.activeInHierarchy == true);
    }

    private IEnumerable<Resource> GetAllResources()
    {
        List<Resource> resources = new List<Resource>();

        for (int i = 0; i < _container.childCount; i++)
        {
            resources.Add(_container.GetChild(i).GetComponent<Resource>());
        }

        return resources;
    }
}
