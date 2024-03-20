using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ParticleSystem _particlePrefab;

    private Queue<ParticleSystem> _pool;

    private void Awake()
    {
        _pool = new Queue<ParticleSystem>();
    }

    public ParticleSystem GetParticle()
    {
        if (_pool.Count == 0)
        {
            ParticleSystem particle = Instantiate(_particlePrefab, transform.position, transform.rotation);
            particle.transform.parent = _container;            

            return particle;
        }

        return _pool.Dequeue();
    }

    public void PutResource(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
        particle.transform.parent = _container;
        _pool.Enqueue(particle);
    }
}
