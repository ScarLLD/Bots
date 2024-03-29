using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private int _basePrice;
    [SerializeField] private GameObject _flagPrefab;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Camera _camera;

    private bool isBuilding = false;

    public void Interect()
    {
        ShowTemplate();
    }

    private void ShowTemplate()
    {
        StartCoroutine(RenderTemplate());
    }

    private void BuildBase(Vector3 position)
    {
        if (_wallet.GoldCount >= _basePrice)
        {
            Instantiate(_basePrefab, position, Quaternion.identity, transform);
            _wallet.DecreaseResources(_basePrice);
        }
    }

    private IEnumerator RenderTemplate()
    {
        isBuilding = true;

        GameObject flag = Instantiate(_flagPrefab);

        while (isBuilding)
        {
            RaycastHit hit;
            Ray ray = (_camera.ScreenPointToRay(Input.mousePosition));

            Debug.DrawRay(ray.origin, ray.direction);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("collider ray");

                flag.transform.position = hit.point + new Vector3(0, 0, 1);

                if (Input.GetMouseButtonDown(0) == true)
                {
                    BuildBase(hit.transform.position);
                    isBuilding = false;
                }
            }

            yield return null;
        }
    }
}
