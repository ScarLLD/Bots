using UnityEngine;

public class Gold : MonoBehaviour
{
    private bool _isTake = false;

    public bool IsTake => _isTake;

    public void ChangeStatus()
    {
        _isTake = !_isTake;
    }
}
