using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isGrub = false;

    public bool IsGrub => _isGrub;

    public void ChangeGrubBool()
    {
        _isGrub = !_isGrub;
    }
}
