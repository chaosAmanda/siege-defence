using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehavoiur : MonoBehaviour
{
    public GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam.GetComponent<CameraScript>().newWindow(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
