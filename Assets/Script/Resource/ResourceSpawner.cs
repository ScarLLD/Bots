using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetwenSpawn;
    [SerializeField] private ResourcesStorage _resourceStorage;
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private ResourcePool _resourcePool;

    private WaitForSeconds _wait;
    private List<Transform> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new List<Transform>();
        _wait = new WaitForSeconds(_timeBetwenSpawn);

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
            _spawnPoints.Add(_spawnPointsParent.GetChild(i));

    }

    private void Start()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        while (enabled)
        {
            List<Transform> _tempSpawnPoints = _spawnPoints.ToList();

            int spawnAttemptsCount = Random.Range(1, _spawnPoints.Count);

            for (int i = 0; i < spawnAttemptsCount; i++)
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