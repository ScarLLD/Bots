using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ParticleSystem _goldParticle;

    private List<ParticleSystem> _pool;
    public int GetParticleCount => _pool.Count;
    
    private void Awake()
    {
        _pool = new List<ParticleSystem>();
    }

    public void GetParticle(Vector3 spawnPoint)
    {
        if (TryGetParticle(out ParticleSystem particle))
        {
            particle.transform.parent = _container;
            particle.transform.position = spawnPoint;
            particle.gameObject.SetActive(true);

        }
        else
        {
            ParticleSystem goldStorage = Instantiate(_goldParticle, spawnPoint, Quaternion.identity);

            _pool.Add(goldStorage);

            goldStorage.transform.parent = _container;
            goldStorage.gameObject.SetActive(true);
        }
    }

    public bool TryGetParticle(out ParticleSystem particle)
    {
        particle = _pool.FirstOrDefault(particle => particle.gameObject.activeInHierarchy == false);
        return particle != null;
    }
}
