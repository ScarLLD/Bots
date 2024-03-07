using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Gold _goldPrefab;

    private List<Gold> _pool;

    public void Reset()
    {
        var timePool = _pool.Where(enemy => enemy.gameObject.activeInHierarchy == true).ToList();

        for (int i = 0; i < timePool.Count; i++)
        {
            timePool[i].gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        _pool = new List<Gold>();
    }

    public void GetGold(Vector3 spawnPoint)
    {
        if (TryGetGold(out Gold gold))
        {
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

    public int GetGoldCount => _pool.Count;

    private bool TryGetGold(out Gold gold)
    {
        gold = _pool.FirstOrDefault(gold => gold.gameObject.activeInHierarchy == false);
        return gold != null;
    }
}
