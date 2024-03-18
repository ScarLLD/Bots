using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Resource _goldPrefab;
    [SerializeField] private ParticlePool _particleGoldPool;

    public event Action Collected;

    private List<Resource> _pool;

    private void Awake()
    {
        _pool = new List<Resource>();
    }

    public int GetResourceCount() => GetActiveResources().Count();

    public void GetGold(Vector3 spawnPoint)
    {
        if (TryGetResource(out Resource gold))
        {
            gold.transform.parent = _container;
            gold.transform.position = spawnPoint;
            gold.gameObject.SetActive(true);

        }
        else
        {
            Resource goldStorage = Instantiate(_goldPrefab, spawnPoint, Quaternion.identity);

            _pool.Add(goldStorage);

            goldStorage.transform.parent = _container;
            goldStorage.gameObject.SetActive(true);
        }
    }

    public IEnumerable<Resource> GetActiveResources()
    {
        return _pool.Where(gold => gold.gameObject.activeInHierarchy == true);
    }

    public bool TryGetResource(out Resource gold)
    {
        gold = _pool.FirstOrDefault(gold => gold.gameObject.activeInHierarchy == false);
        return gold != null;
    }

    public bool TryGetNotGrubResource(out Resource gold)
    {
        gold = GetActiveResources().FirstOrDefault(gold => gold.IsGrub == false);
        return gold != null;
    }

    public bool TryGetSpawnPoint(Vector3 tempPosition)
    {
        return GetActiveResources().Any(gold => gold.transform.position == tempPosition);
    }  

    public void CollectGold(Resource gold)
    {
        gold.gameObject.SetActive(false);
        gold.ChangeGrubStatus();
        gold.transform.parent = _container.transform;

        _particleGoldPool.GetParticle(gold.transform.position);

        Collected?.Invoke();
        Debug.Log("Collected");
    }
}
