using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vars : MonoBehaviour {

    
    public int currHP;
    public int maxHP;
    public MeshCollider mc;
    public Rigidbody rb;
    public GameObject go;
    // Use this for initialization
    void Start () {
       
    }
   
    // Update is called once per frame
    void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;
        Debug.Log(System.String.Format("Collision between {0} with {1}",go.name,contact.name));
        if (contact.tag == "Asteroid") {
            vars v = contact.GetComponent<vars>();
            int dmg = Mathf.Max((int)collision.impulse.magnitude/4,1);
            //Dmg to other asteroid
            if (dmg >= v.maxHP)
            {
                Debug.Log("ded");
                Destroy(contact);
            }
            else if (dmg >= currHP)
            {
                Debug.Log("live, my children");
                v.currHP -= dmg;
            }
            else
            {
                Debug.Log("I lived, bitch");
                v.currHP -= dmg;
            }
        }
        else
        {
            Debug.Log("How!?!?!?!?!?");
        }
       
    }
}
