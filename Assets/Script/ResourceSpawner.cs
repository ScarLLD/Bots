using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ResourcePool))]
public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetwenSpawn;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private ParticlePool _particlePool;

    private float _minVector3Distence = 0.001f;
    private ResourcePool _resourcePool;
    private WaitForSeconds _wait;
    private List<Transform> _spawnPoints;
    private Queue<Resource> _resources;

    public event Action ResourceCollected;

    private void Awake()
    {
        _resourcePool = GetComponent<ResourcePool>();

        _resources = new Queue<Resource>();
        _spawnPoints = new List<Transform>();
        _wait = new WaitForSeconds(_timeBetwenSpawn);

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
            _spawnPoints.Add(_spawnPointsParent.GetChild(i));

    }

    private void Start()
    {
        StartCoroutine(Generate());
    }

    public void CollectResource(Resource resource)
    {
        _resourcePool.PutResource(resource);
        SpawnParticle(resource.transform.position);

        ResourceCollected?.Invoke();
    }

    public bool TrySelectResource(out Resource resource)
    {
        resource = null;

        if (_resources.Count > 0)
            resource = _resources.Dequeue();

        return resource != null;
    }

    private void SpawnParticle(Vector3 spawnPosition)
    {
        ParticleSystem particle = _particlePool.GetParticle();

        particle.transform.position = spawnPosition;
        particle.gameObject.SetActive(true);
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            List<Transform> _tempSpawnPoints = _spawnPoints.ToList();

            int goldCount = Random.Range(1, _spawnPoints.Count);

            for (int i = 0; i < goldCount && i <= _tempSpawnPoints.Count; i++)
            {
                int index = Random.Range(0, _tempSpawnPoints.Count - 1);
                Transform spawnPoint = _tempSpawnPoints[index];

                if (_resources.All(resource => (Vector3.Distance
                (resource.transform.position, spawnPoint.transform.position) > _minVector3Distence)))
                {
                    Resource resource = _resourcePool.GetResource();
                    _resources.Enqueue(resource);

                    resource.transform.position = spawnPoint.position;
                    resource.gameObject.SetActive(true);
                }

                _tempSpawnPoints.Remove(spawnPoint);
            }

            yield return _wait;
        }
    }
}