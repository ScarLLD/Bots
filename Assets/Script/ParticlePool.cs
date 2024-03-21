using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private Vector3 _rotation;

    private List<ParticleSystem> _particles;

    private void Awake()
    {
        _particles = new List<ParticleSystem>();
    }

    public ParticleSystem GetParticle()
    {
        if (TryGetParticle(out ParticleSystem particle))
        {
            return particle;
        }
        else
        {
            particle = Instantiate(_particlePrefab, transform.position, transform.rotation);
            particle.transform.Rotate(_rotation);
            particle.transform.parent = _container;

            _particles.Add(particle);

            return particle;
        }
    }

    private bool TryGetParticle(out ParticleSystem particle)
    {
        particle = _particles.FirstOrDefault(particle => particle.gameObject.activeInHierarchy == false);

        return particle != null;
    }
}
