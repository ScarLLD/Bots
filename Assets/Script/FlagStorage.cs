using System.Collections.Generic;
using UnityEngine;

public class FlagStorage : MonoBehaviour
{
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private SheltersSpawner _sheltersSpawner;

    public Queue<Flag> Flags { get; private set; }

    private void TakeFlag(Flag flag) => Flags.Enqueue(flag);

    private void OnEnable()
    {
        _flagSetter.FlagInstalled += TakeFlag;
        _sheltersSpawner.FlagRemoved += RemoveFlag;
    }

    private void OnDisable()
    {
        _flagSetter.FlagInstalled -= TakeFlag;
        _sheltersSpawner.FlagRemoved -= RemoveFlag;
    }

    private void Awake()
    {
        Flags = new Queue<Flag>();
    }

    private bool TryTakeFlag(out Flag flag)
    {
        flag = null;

        if (Flags.Count > 0)
        {
            flag = Flags.Dequeue();
        }

        return flag != null;
    }
}
