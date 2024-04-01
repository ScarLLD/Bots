using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollector))]
public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private int _basePrice;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LayerMask _hitMask;

    private Coroutine _templateCoroutine;
    private Collider[] _colliders;
    private bool _isBuildingTemplate = false;


    public void Interect()
    {
        ShowTemplate();
    }

    private void ShowTemplate()
    {
        if (_templateCoroutine == null)
            _templateCoroutine = StartCoroutine(RenderTemplate());
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
        _isBuildingTemplate = true;

        Flag flag = Instantiate(_flagPrefab);

        RaycastHit hit;
        Ray ray;

        while (_isBuildingTemplate)
        {
            ray = (_camera.ScreenPointToRay(Input.mousePosition));

            Debug.DrawRay(ray.origin, ray.direction * 100);

            if (Physics.Raycast(ray, out hit, 100f, _hitMask))
            {
                flag.transform.position = hit.point + new Vector3(0, 0.1f, 0);

                _colliders = Physics.OverlapBox(flag.transform.position, flag.transform.localScale * 6, Quaternion.identity);

                if (_colliders.All(collider => collider.GetComponent<LevelPlane>()))
                    flag. .SetActive(true);
                else
                    flag.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0) == true && flag.gameObject.activeInHierarchy == true)
                {
                    BuildBase(hit.transform.position);
                    _isBuildingTemplate = false;
                }
            }

            yield return null;
        }
    }
}
