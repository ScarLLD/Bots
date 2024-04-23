using UnityEngine;

public class FlagStorage : MonoBehaviour
{
    private Flag _flag;

    public void RemoveFlag()
    {
        Destroy(_flag.gameObject);
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
}
