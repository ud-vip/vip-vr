using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SphereMover : NetworkBehaviour {

	const float SPEED = 1.0f;

	// Use this for initialization
	void Start () {
		
	}


    // Update is called once per frame
    void Update () {
        //Stop others from controlling the wrong player
        if (! isLocalPlayer)
         {
             return;
         }

       /* var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);*/

        /*if (Input.GetKey("up")){
			transform.Translate(SPEED * Vector3.forward * Time.deltaTime);
		}
		if (Input.GetKey("down")){
			transform.Translate(SPEED * Vector3.back * Time.deltaTime);
		}
		if (Input.GetKey("left")){
			transform.Translate(SPEED * Vector3.left * Time.deltaTime);
		}
		if (Input.GetKey("right")){
			transform.Translate(SPEED * Vector3.right * Time.deltaTime);
		}*/
    }
    public override void OnStartLocalPlayer()
    {
        print("Yarg");
       // GetComponent<Material>().color = Color.red;
    }
}
