using System;
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
    [SerializeField] private int _rayDirection;
    [SerializeField] private LayerMask _hitMask;

    private BaseCollector _baseCollector;
    private Collider[] _colliders;
    private Flag _tempflag;
    private bool _isWork;

    public int GetBasePrice => _basePrice;

    private void Awake()
    {
        _baseCollector = GetComponent<BaseCollector>();
    }

    public void ShowTemplate(Base tempBase)
    {
        if (_isWork == false)
            StartCoroutine(Show(tempBase));
    }

    public void BuildBase(Vector3 tempPosition, Unit unit)
    {
        Base tempBase = Instantiate(_basePrefab, tempPosition, Quaternion.identity, transform);
        tempBase.TakeUnit(unit);


        _wallet.DecreaseResources(_basePrice);
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

    private IEnumerator Show(Base tempBase)
    {
        _isWork = true;

        Flag flag = Instantiate(_flagPrefab, transform);

        BoxCollider flagCollider = flag.GetComponent<BoxCollider>();

        flagCollider.enabled = false;

        Ray ray;

        while (_isWork)
        {
            ray = (_camera.ScreenPointToRay(Input.mousePosition));

            if (Physics.Raycast(ray, out RaycastHit hit, _rayDirection, _hitMask))
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

                    tempBase.WaitRequire(_tempflag);

                    _isWork = false;
                }
            }

            yield return null;
        }

        Destroy(flag.gameObject);
    }
}
