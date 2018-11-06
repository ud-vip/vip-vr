using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectABox : MonoBehaviour {
	public GameObject[] cubes = new GameObject [4];
	int currentCube = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//float x = Input.GetAxis (4); //Right trackpad horizontal
		//float y = Input.GetAxis (5); //Right trackpad vertical
		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");
		Debug.Log ("x: " + x + " y: " + y);
		int select = getCube (x, y);
		if (select == -1) {
			foreach (GameObject c in cubes)
				unSetCubeColor (c);
		} else {
			if (currentCube != -1 && cubes [currentCube] != cubes [select])
				unSetCubeColor (cubes [currentCube]);
			setCubeColor (cubes [select]);
			currentCube = select;
		}
	}

	void setCubeColor(GameObject cube){

		if (cube.GetComponent<Renderer> ())
			cube.GetComponent<Renderer> ().material.color = Color.red;
	}

	void unSetCubeColor(GameObject cube){
		if (cube.GetComponent<Renderer> ())
			cube.GetComponent<Renderer> ().material.color = Color.white;
	}

	int getCube(float x, float y){
		if (x < 0) {
			if (y < 0.66)
				return 0; //Leftmost
			else
				return 1; //2nd left
		} else if (x > 0) {
			if (y < 0.66)
				return 3; //Rightmost
			else
				return 2; //2nd right
		} else {
			return -1;
		}
	}
}
