using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsGrub { get; private set; } = false;

    public void ChangeGrubBool()
    {
        IsGrub = !IsGrub;
    }
}
