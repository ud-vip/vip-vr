using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projBehavior : MonoBehaviour {

	// Use this for initialization
	public float speed;
	public GameObject ship;
	public Vector3  direction;
	public float duration;
	void Start () {
		ship = GameObject.Find("TestShip");
		 direction = (ship.transform.position - transform.position).normalized;
		
		 
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed*Time.deltaTime;
		duration -= 1*Time.deltaTime;
		//transform.position = Vector3.MoveTowards(transform.position,direction,step);
		transform.position += direction * step;
		if(duration<=0){
			Destroy(gameObject);
		}

		



	}

	void OnCollisionEnter(Collision otherObject){
		Destroy(gameObject);

	}
}
