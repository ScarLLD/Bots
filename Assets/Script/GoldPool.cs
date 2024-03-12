using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoldPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Gold _goldPrefab;

    private List<Gold> _pool;

    public int GetGoldCount => _pool.Count;

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

    public void SetContainerParent(Gold gold)
    {
        gold.transform.parent = _container;
    }

    public bool TryGetGold(out Gold gold)
    {
        gold = _pool.FirstOrDefault(gold => gold.gameObject.activeInHierarchy == true);
        return gold != null;
    }

    public bool TryGetAvailableGold(out Gold gold)
    {
        gold = _pool.Where(gold => gold.gameObject.activeInHierarchy == true).FirstOrDefault(gold => gold.IsTake == false);
        return gold != null;
    }
}
