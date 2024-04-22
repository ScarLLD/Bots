using System.Collections.Generic;
using UnityEngine;

public class FlagsStorage : MonoBehaviour
{
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private SheltersSpawner _sheltersSpawner;

    public List<Flag> Flags { get; private set; }

    private void OnEnable()
    {
        _flagSetter.FlagInstalled += TakeFlag;
        _sheltersSpawner.FlagRemoved += Remove;
    }

    private void OnDisable()
    {
        _flagSetter.FlagInstalled -= TakeFlag;
        _sheltersSpawner.FlagRemoved -= Remove;

    }

    private void Awake()
    {
        Flags = new List<Flag>();
    }

    private void TakeFlag(Flag flag)
    {
        Flags.Add(flag);
    }

    private void Remove(Flag flag)
    {
        Flags.Remove(flag);
        Destroy(flag.gameObject);
    }
}
