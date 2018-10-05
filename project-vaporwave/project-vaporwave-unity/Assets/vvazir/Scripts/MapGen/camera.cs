using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    Camera cam;
    public float speed = 10;
    public float sensitivity = 0.01f;
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate camera
        Vector3 vp = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity;
        vp.y *= sensitivity;
        vp.x += 0.5f;
        vp.y += 0.5f;
        Vector3 sp = cam.ViewportToScreenPoint(vp);

        Vector3 v = cam.ScreenToWorldPoint(sp);
        transform.LookAt(v, Vector3.up);
        
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        if (Camera.current != null)
        {
            Camera.current.transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                if (objectHit.tag == "Asteroid")
                {
                    objectHit.GetComponent<Rigidbody>().useGravity = true;
                }
                
            }
        }
    }
}
