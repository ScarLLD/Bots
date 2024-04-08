using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseCollector))]
public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _rayDirection;
    [SerializeField] private LayerMask _hitMask;

    private Ray _ray;
    private Collider[] _colliders;
    private Flag _tempflag;
    private bool _isWork;

    public Flag GetTempFlag() => _tempflag;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_isWork == false && Physics.Raycast(_ray, out RaycastHit _hit))
            if (Input.GetMouseButtonDown(0) && _hit.transform.gameObject.TryGetComponent(out Base tempbase))
                StartCoroutine(ShowTemplate(tempbase));

    }

    private void SetTemplate(Flag flag)
    {
        if (_tempflag != null)
        {
            _tempflag.transform.position = flag.transform.position;
        }
        else
        {
            _tempflag = Instantiate(_flagPrefab, flag.transform.position, flag.transform.rotation, transform);
        }
    }

    private IEnumerator ShowTemplate(Base tempBase)
    {
        _isWork = true;

        Flag flag = Instantiate(_flagPrefab, transform);

        BoxCollider flagCollider = flag.GetComponent<BoxCollider>();

        flagCollider.enabled = false;

        while (_isWork)
        {
            if (Physics.Raycast(_ray, out RaycastHit hit, _rayDirection, _hitMask))
            {
                flag.transform.position = new Vector3(hit.point.x, tempBase.transform.position.y, hit.point.z);

                _colliders = Physics.OverlapBox(flag.transform.position, flag.transform.localScale * 6, Quaternion.identity);

                if (_colliders.All(collider => collider.GetComponent<LevelPlane>()))
                    flag.gameObject.SetActive(true);
                else
                    flag.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0) == true && flag.gameObject.activeInHierarchy == true)
                {
                    flagCollider.enabled = true;

                    SetTemplate(flag);

                    tempBase.TakeFlag(_tempflag);

                    _isWork = false;
                }
            }

            yield return null;
        }

        Destroy(flag.gameObject);
    }
}
