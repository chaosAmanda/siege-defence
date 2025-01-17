using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.WSA;
using Unity.VisualScripting;
//using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

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
    public GameObject TouchTarget;
    [SerializeField] List<GameObject> windows;

    private Ray ray;
    

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
    //code modified to use raycastAll isnstead of raycast
    //this returns an array of all hits rather than the fist object hit by the ray
    //the colliders are then checked for isTrigger == True, which
    //prevents the ray interacting with other colliders, such as the attack collider of the basic defender 
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, 10);
        hit = default;
        foreach (RaycastHit hitOb in hits)
        {
            //Debug.Log(hitOb.collider.gameObject.tag);
            if (hitOb.collider.isTrigger)
            {
                if (!isPointerOverUIObject()) { target = hitOb.collider.gameObject; }
                Debug.Log("hit trigger");
                return target;
            }
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

    public void newWindow(GameObject window)
    {
        windows.Add(window);
    }


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
                /*foreach (GameObject window in windows)
                {
                    if (window != null)
                    {

                        if (window == GetClickedObject(out RaycastHit hit))
                        {
                            menu = true;
                            GameManager.GetComponent<GameManager>().window(window);
                        }
                    }
                }*/
                TouchTarget = GetClickedObject(out RaycastHit hit);
                if (TouchTarget != null)
                {
                    if (TouchTarget.tag == "window")
                    {
                        menu = true;
                        GameManager.GetComponent<GameManager>().window(TouchTarget);
                    }
                    else if (TouchTarget.tag == "defender")
                    {

                    }
                }
            }

        }



        //RotateCamera(yawRotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ray.origin, ray.origin + (ray.direction*10f));
    }
}
