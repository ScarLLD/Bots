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
    [SerializeField] private LayerMask _resourceLayer;

    private ResourcePool _resourcePool;
    private ResourcesStorage _resourceStorage;
    private WaitForSeconds _wait;
    private List<Transform> _spawnPoints;

    public event Action ResourceCollected;

    private void Awake()
    {
        _resourcePool = GetComponent<ResourcePool>();
        _resourceStorage = GetComponent<ResourcesStorage>();

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

            int TrySpawnCount = Random.Range(1, _spawnPoints.Count);

            for (int i = 0; i < TrySpawnCount; i++)
            {
                int index = Random.Range(0, _tempSpawnPoints.Count - 1);
                Transform spawnPoint = _tempSpawnPoints[index];
                SphereCollider[] hitColliders = new SphereCollider[1];

                int collidersCount = Physics.OverlapSphereNonAlloc
                    (spawnPoint.position, 1, hitColliders, _resourceLayer);

                if (collidersCount == 0)
                {
                    Resource resource = _resourcePool.GetResource();
                    _resourceStorage.TakeResource(resource);

                    resource.transform.position = spawnPoint.position;
                    resource.gameObject.SetActive(true);
                }

                _tempSpawnPoints.Remove(spawnPoint);
            }

            yield return _wait;
        }
    }
}