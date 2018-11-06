using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSquid : MonoBehaviour {

	// Use this for initialization
	public GameObject squid;
	public GameObject ship;
	public GameObject projectile;
	public float speed;
	public float driftTime;
	public float timer;
	public float attackTime;
	private bool projBuffer;
	private bool inShipRange;
	public float range;
	private bool objAvoidBuffer;
	public Vector3 avoidVector;
	void Start () {
		ship = GameObject.Find("TestShip");
		projBuffer = false;
		inShipRange = false;
		objAvoidBuffer = false;
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		timer += Time.deltaTime;

		//A timer is used to control the duration of the two phases of movements for the squid, driftTime and attackTime
		if(timer > driftTime){ //When the timer finishes the driftTime then the squid moves towards the ship for a duration of attackTime
			//If the squid is within the set range of the ship, then the squid moves away from the ship
			if(inShipRange==true){
				awayShip(step);
			}	
			else{
				if(objAvoidBuffer==false){
					toTheShip(step);
				}
				else{
					squid.transform.position += avoidVector*step*3;
				}
				}	

			if(timer>attackTime){ //After the duration of attackTime, the timer and projectile buffer reset
					timer = 0f;
					projBuffer = false;
					objAvoidBuffer = false;
					if(Vector3.Distance(squid.transform.position,ship.transform.position)<range){
						inShipRange = true;
					}
					else inShipRange = false;
			}

			//THIS IS THE PROJECTILE CODE(WORK IN PROGRESS)
			if(projBuffer==false){ //This makes sure only one projectile is fired during a phase of the squids movement
				//Creating Raycast
				Ray ray = new Ray(squid.transform.position, squid.transform.forward);
				RaycastHit hit;

				//If the raycast hits an object, a projectile is launched
				if (Physics.Raycast(ray.origin,ray.direction, out hit, 10000f)) {
					 //PUT IN PROJECTILE FIRING ANIMATION HERE
					 if((hit.transform.gameObject != ship.transform.gameObject) && (hit.distance < 15)){
						 objAvoidBuffer = true;
						 avoidVector = hit.normal;
					 }
					 else{
						 GameObject tempProj = Instantiate(projectile,squid.transform.position,squid.transform.rotation);
						Physics.IgnoreCollision(tempProj.GetComponent<Collider>(), squid.GetComponent<Collider>());
					 }
					

					 projBuffer = true;	 
				 }
				 else projBuffer = false;
			}

		}
		else{ //While the timer is less than the driftTime, the squid drifts away from the ship
			Vector3 squidDown = new Vector3(squid.transform.position.x,squid.transform.position.y-10,squid.transform.position.z);
			squid.transform.position = Vector3.MoveTowards(squid.transform.position,squidDown,(step*2)/3);
			squid.transform.LookAt(ship.transform.position, ship.transform.forward);
		}     
				  
			
		
	}

	void toTheShip(float step){
		squid.transform.position = Vector3.MoveTowards(squid.transform.position, ship.transform.position, step*3);
	}
	
	void awayShip(float step){
		Vector3 squidInverse = new Vector3(-ship.transform.position.x,-ship.transform.position.y,-ship.transform.position.z);
		squid.transform.position = Vector3.MoveTowards(squid.transform.position, squidInverse, step*3);
	}


}
	



