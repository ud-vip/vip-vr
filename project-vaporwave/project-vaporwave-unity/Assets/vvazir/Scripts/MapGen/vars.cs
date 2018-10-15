using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vars : MonoBehaviour {

    
    public int currHP;
    public int maxHP;
    public GameObject exploded;
    public GameObject internalExploded;
    public MeshCollider mc;
    public Rigidbody rb;
    public GameObject go;
    public GameObject main;
    public float radius = 20;
    public int idx;
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

                Debug.Log(System.String.Format("{0} is ded", contact.transform.name));
                v.internalExploded = Instantiate(v.exploded, contact.transform.position,contact.transform.rotation,main.transform);
                v.internalExploded.transform.localScale = contact.transform.localScale;
                foreach (Transform t in v.internalExploded.transform)
                {
                    t.gameObject.GetComponent<shards>().force = dmg / 4;
                    t.gameObject.GetComponent<shards>().currHP = (int)contact.gameObject.transform.GetComponent<Rigidbody>().mass / 12;
                    t.gameObject.GetComponent<shards>().maxHP = (int)contact.gameObject.transform.GetComponent<Rigidbody>().mass / 12;
                    t.gameObject.GetComponent<shards>().direction = transform.GetComponent<Rigidbody>().velocity;
                    t.gameObject.GetComponent<shards>().start = collision.contacts[0].point;
                    
                    t.gameObject.GetComponent<Rigidbody>().mass = contact.gameObject.transform.GetComponent<Rigidbody>().mass / 25;
                }
                contact.SetActive(false);
                //Explode(dmg/100f);
                Destroy(contact);
                
            }
            else if (dmg >= v.currHP)
            {
                Debug.Log("live, my children");
                int count = (int)Random.Range(1, 5);

                float size = contact.GetComponent<Renderer>().bounds.size.magnitude/count;
                
                Destroy(contact);
            }
            else
            {
                Debug.Log("I lived, bitch");
                v.currHP -= dmg;
            }
        }
        else if(contact.tag=="Asteroid-shard")
        {
            shards v = contact.GetComponent<shards>();
            int dmg = Mathf.Max((int)collision.impulse.magnitude / 4, 1);
            if (dmg >= v.currHP)
            {

                Debug.Log(System.String.Format("{0} is ded", contact.transform.name));
                //internalExploded = Instantiate(exploded, contact.transform.position, contact.transform.rotation, main.transform);
                //internalExploded.transform.localScale = contact.transform.localScale;
                Destroy(contact);
            }
           
            else
            {
                Debug.Log("I lived, bitch");
                v.currHP -= dmg;
            }
        }
        else if (contact.tag == "skybox")
        {
            Debug.Log("Goodbye cruel world");
        }
        else
        {
            Debug.Log("How!?!?!?!?!?");
        }
       
    }
}
