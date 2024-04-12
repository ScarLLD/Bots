using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _goldPrefab;
    [SerializeField] private ParticlePool _particlePool;

    public Queue<Resource> Pool { get; private set; }

    public event Action ResourceCollected;

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

    public void CollectResource(Resource resource)
    {
        PutResource(resource);
        resource.ChangeGrubBool();

        SpawnParticle(resource.transform.position);

        ResourceCollected?.Invoke();
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

    public IEnumerable<Resource> GetActiveResources()
    {
        return GetAllResources().Where(resource => resource.gameObject.activeInHierarchy == true);
    }

    private void SpawnParticle(Vector3 spawnPosition)
    {
        ParticleSystem particle = _particlePool.GetParticle();

        particle.transform.position = spawnPosition;
        particle.gameObject.SetActive(true);
    }

    private IEnumerable<Resource> GetAllResources()
    {
        List<Resource> resources = new();

        for (int i = 0; i < _container.childCount; i++)
        {
            resources.Add(_container.GetChild(i).GetComponent<Resource>());
        }

        return resources;
    }
}
