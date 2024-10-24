using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    [SerializeField] Transform _target;
    [SerializeField] GameObject _targetObject;
    [SerializeField] float _distanceFromTarget = 10.0f;

    public float RotationSensitivity = 100.0f;
    public float VerticalSensitivity = 1.0f;

    public float maxHeight = 10.0f;
    public float minHeight = 0.0f;

    private float _yaw = 0.0f;
    private float _pitch = 0.0f;

    public bool menu = false;
    [SerializeField] GameObject window;
    

    public void HandleInput()
    {
        Vector2 inputDelta = Vector2.zero;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputDelta = touch.deltaPosition;
        }



        _yaw += inputDelta.x * RotationSensitivity * Time.deltaTime;
        _pitch -= inputDelta.y * VerticalSensitivity * Time.deltaTime;
    }

    void RotateCamera(Quaternion Rotation)
    {
        Vector3 positionOffset = Rotation* new Vector3(0, 0, -_distanceFromTarget);
        transform.position = _target.position + positionOffset;
        transform.rotation = Rotation;
    }


    //tutorial code: https://www.youtube.com/watch?v=pfkQDGhd8_A&t=30s&ab_channel=XSGames%F0%9F%8C%80FrankEno
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }
    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }

    //end of tutorial code


// Update is called once per frame
void Update()
    {
        if (!menu)
        {
            _pitch = 0.0f;

            HandleInput();

            Quaternion yawRotation = Quaternion.Euler(_target.position.x, _yaw, 0.0f);
            RotateCamera(yawRotation);



            _target.transform.position = new Vector3(0.0f, _target.transform.position.y + _pitch, 0.0f);

            if (_target.transform.position.y < minHeight)
            {
                _target.transform.position = new Vector3(0.0f, minHeight, 0.0f);
            }
            else if (_target.transform.position.y > maxHeight)
            {
                _target.transform.position = new Vector3(0.0f, maxHeight, 0.0f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (window == GetClickedObject(out RaycastHit hit))
                {
                    menu = true;
                    GameManager.GetComponent<GameManager>().window(window);
                }
            }
        }



        //RotateCamera(yawRotation);
    }
}
