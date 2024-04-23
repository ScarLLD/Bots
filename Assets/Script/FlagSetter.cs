using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private ShelterButtonTracker _shelterButtonTracker;
    [SerializeField] private FlagStorage _flagStorage;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private int _rayDirection;
    [SerializeField] private LayerMask _hitLayer;

    private Camera _camera;
    private Collider[] _colliders;
    private Ray _ray;
    private bool _isWork;

    private void OnEnable()
    {
        _shelterButtonTracker.ButtonClicked += ShowFlag;
    }

    private void OnDisable()
    {
        _shelterButtonTracker.ButtonClicked -= ShowFlag;
    }

    public void Init(Camera camera)
    {
        _camera = camera;
    }

    private void ShowFlag()
    {
        StartCoroutine(ShowTemplate());
    }

    private IEnumerator ShowTemplate()
    {
        _isWork = true;

        Vector3 positionMultiple = _flagPrefab.transform.position;

        Flag flag = Instantiate(_flagPrefab, _flagStorage.transform);

        BoxCollider flagCollider = flag.GetComponent<BoxCollider>();

        flagCollider.enabled = false;

        while (_isWork)
        {
            Debug.Log("Ray");

            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, _rayDirection, _hitLayer))
            {
                flag.transform.position = new Vector3(hit.point.x,
                    positionMultiple.y, hit.point.z);

                _colliders = Physics.OverlapBox(flag.transform.position,
                    flag.transform.localScale * 6, Quaternion.identity);

                if (_colliders.All(collider => collider.GetComponent<LevelPlane>()))
                    flag.gameObject.SetActive(true);
                else
                    flag.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0) == true
                    && flag.gameObject.activeInHierarchy == true)
                {
                    flagCollider.enabled = true;

                    _flagStorage.PutUpFlag(flag);

                    _isWork = false;
                }
            }

            yield return null;
        }

        Destroy(flag.gameObject);
    }
}
