using UnityEngine;

public class FlagStorage : MonoBehaviour
{
    private Flag _flag;

    public void RemoveFlag()
    {
        _flag = null;
    }

    public void PutUpFlag(Flag flag)
    {
        if (_flag == null)
        {
            _flag = Instantiate(flag, transform);
        }

        _flag.transform.position = flag.transform.position;
    }

    public bool TryGetFlag(out Flag flag)
    {
        flag = null;

        if (_flag != null)
            flag = _flag;

        return flag != null;
    }
}
