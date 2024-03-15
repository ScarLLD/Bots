using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Gold _goldPrefab;
    [SerializeField] private ParticleGoldPool _particleGoldPool;

    public event Action PoolEmpty;
    public event Action Collected;

    private List<Gold> _pool;

    public void Reset()
    {
        var timePool = _pool.Where(gold => gold.gameObject.activeInHierarchy == true).
            Select(gold => { gold.gameObject.SetActive(false); return gold; });
    }

    private void Awake()
    {
        _pool = new List<Gold>();
    }

    public void GetGold(Vector3 spawnPoint)
    {
        if (TryGetGold(out Gold gold))
        {
            gold.transform.parent = _container;
            gold.transform.position = spawnPoint;
            gold.gameObject.SetActive(true);

        }
        else
        {
            Gold goldStorage = Instantiate(_goldPrefab, spawnPoint, Quaternion.identity);

            _pool.Add(goldStorage);

            goldStorage.transform.parent = _container;
            goldStorage.gameObject.SetActive(true);
        }
    }

    public bool TryGetGold(out Gold gold)
    {
        gold = _pool.FirstOrDefault(gold => gold.gameObject.activeInHierarchy == false);
        return gold != null;
    }

    public bool TryGetAvailableGold(out Gold gold)
    {
        gold = _pool.Where(gold => gold.gameObject.activeInHierarchy == true).FirstOrDefault(gold => gold.IsGrub == false);
        return gold != null;
    }

    public bool TryGetPoint(Vector3 tempPosition)
    {
        var isPoint = _pool.Where(gold => gold.gameObject.activeInHierarchy == false).
            Any(gold => gold.transform.position == tempPosition);

        return isPoint;
    }

    public int GetGoldCount()
    {
        return _pool.Where(gold => gold.gameObject.activeInHierarchy == true).Count();
    }

    public void CollectGold(Gold gold)
    {
        gold.gameObject.SetActive(false);
        gold.ChangeGrubStatus();
        gold.transform.parent = _container.transform;
        _particleGoldPool.GetParticle(gold.transform.position);
        Collected?.Invoke();

        if (GetGoldCount() == 0)
            PoolEmpty?.Invoke();
    }    
}
