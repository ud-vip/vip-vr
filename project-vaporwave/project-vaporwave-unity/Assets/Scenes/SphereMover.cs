using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour {

	const float SPEED = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("up")){
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
		}
	}
}
