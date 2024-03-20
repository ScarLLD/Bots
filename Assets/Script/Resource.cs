using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isGrub = false;
    private bool _isTaken = false;

    public bool IsGrub => _isGrub;
    public bool IsTaken => _isTaken;

    public void ChangeGrubBool()
    {
        _isGrub = !_isGrub;
    }
}
