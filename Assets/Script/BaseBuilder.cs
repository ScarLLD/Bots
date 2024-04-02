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

    private BaseCollector _baseCollector;
    private Collider[] _colliders;
    private Flag _tempflag;
    private bool _isWork;

    private void Awake()
    {
        _baseCollector = GetComponent<BaseCollector>();
    }

    public void Interect()
    {
        ShowTemplate();
    }

    private void ShowTemplate()
    {
        if (_isWork == false)
            StartCoroutine(RenderTemplate());
    }

    private void BuildBase(Vector3 tempPosition)
    {
        float positionY = _baseCollector.GetStartPosition.y;

        if (_wallet.GoldCount >= _basePrice)
        {
            Instantiate(_basePrefab, new Vector3(tempPosition.x, positionY, tempPosition.z),
                Quaternion.identity, transform);
            _wallet.DecreaseResources(_basePrice);
        }
    }

    private IEnumerator RenderTemplate()
    {
        _isWork = true;

        Flag flag = Instantiate(_flagPrefab);

        Ray ray;

        while (_isWork)
        {
            ray = (_camera.ScreenPointToRay(Input.mousePosition));

            Debug.DrawRay(ray.origin, ray.direction * 100);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _hitMask))
            {
                flag.transform.position = hit.point + new Vector3(0, 0.1f, 0);

                _colliders = Physics.OverlapBox(flag.transform.position, flag.transform.localScale * 6, Quaternion.identity);

                if (_colliders.All(collider => collider.GetComponent<LevelPlane>()))
                    flag.gameObject.SetActive(true);
                else
                    flag.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0) == true && flag.gameObject.activeInHierarchy == true)
                {
                    if (_tempflag != null)
                        _tempflag.transform.position = flag.transform.position;
                    else
                        _tempflag = Instantiate(_flagPrefab, flag.transform.position, flag.transform.rotation);

                    //BuildBase(_tempflag.transform.position);
                    _isWork = false;
                }
            }

            yield return null;
        }

        Destroy(flag.gameObject);

        Debug.Log("FlagState");
    }
}
