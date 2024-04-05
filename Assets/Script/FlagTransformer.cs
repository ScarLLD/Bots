using System.Collections;
using System.Linq;
using UnityEngine;

public class FlagTransformer : MonoBehaviour
{
    private Collider[] _colliders;

    public bool IsTrueColliders = false;

    public void InterectColliders(Flag flag)
    {
        StartCoroutine(InitColliders(flag));
    }

    private IEnumerator InitColliders(Flag flag)
    {
        bool isWork = true;

        while (isWork)
        {
            _colliders = Physics.OverlapBox(flag.transform.position, flag.transform.localScale * 6, Quaternion.identity);

            if (_colliders.All(collider => collider.GetComponent<LevelPlane>()))
                IsTrueColliders = flag;
            else
                flag.gameObject.SetActive(false);

            yield return null;
        }
    }
}
