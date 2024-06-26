using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SheltersSpawner))]
public class FlagSetter : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _rayDirection;
    [SerializeField] private LayerMask _hitLayer;

    private Collider[] _colliders;
    private Ray _ray;
    private bool _isWork;

    public event Action<Flag> FlagInstalled;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_isWork == false && Physics.Raycast(_ray, out RaycastHit _hit))
            if (Input.GetMouseButtonDown(0)
                && _hit.transform.gameObject.TryGetComponent(out Shelter shelter))
                if (shelter.UnitsCount > 1)
                    StartCoroutine(ShowTemplate(shelter));
    }

    private void SetFlag(Shelter shelter, Flag flag)
    {
        if (shelter.Flag != null)
        {
            shelter.Flag.transform.position = flag.transform.position;
        }
        else
        {
            Flag tempFlag = Instantiate(_flagPrefab, flag.transform.position,
                flag.transform.rotation, transform);

            FlagInstalled?.Invoke(tempFlag);

            shelter.GiveBuildTask(tempFlag);
        }
    }

    private IEnumerator ShowTemplate(Shelter shelter)
    {
        _isWork = true;

        Vector3 positionMultiple = _flagPrefab.transform.position;

        Flag flag = Instantiate(_flagPrefab, transform);

        BoxCollider flagCollider = flag.GetComponent<BoxCollider>();

        flagCollider.enabled = false;

        while (_isWork)
        {
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

                    SetFlag(shelter, flag);

                    _isWork = false;
                }
            }

            yield return null;
        }

        Destroy(flag.gameObject);
    }
}
