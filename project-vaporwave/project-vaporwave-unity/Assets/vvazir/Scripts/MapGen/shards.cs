using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shards : MonoBehaviour
{
    public GameObject shard;
    public Vector3 direction;
    public Vector3 start;
    public GameObject main;
    public int force;
    public int currHP;
    public int maxHP;
    
    // Use this for initialization
    void Start()
    {
        shard = transform.gameObject;
        main = GameObject.FindGameObjectWithTag("GameController");
        MeshCollider mc = shard.GetComponent<MeshCollider>();
        //mc.enabled = false;
    }
    private void OnEnable()
    {
        //Invoke("EnableMC", .1f);
        if (shard == null)
        {
            shard = transform.gameObject;
        }
        var rb = shard.GetComponent<Rigidbody>();
        if (rb != null) //&& shard.tag == "Asteroid-shard")
        {
            rb.useGravity = false;
            rb.angularDrag = 0;
            rb.AddExplosionForce(Random.Range(force / 2, force*2), start, 2);
            rb.AddForce(direction,ForceMode.Impulse);
            //rb.AddExplosionForce(300, shard.transform.position, 20);
            rb.mass = rb.mass / shard.GetComponent<Renderer>().bounds.size.magnitude;
        }
        else
        {
            // No RB, cant affect it, kill it.
            //Destroy(transform.gameObject);
        }
    }
    private void EnableMC()
    {
        MeshCollider mc = shard.GetComponent<MeshCollider>();
        mc.enabled = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;
        if (contact.tag == "Asteroid")
        {
            vars v = contact.GetComponent<vars>();
            int dmg = Mathf.Max((int)collision.impulse.magnitude / 4, 1);
            //Dmg to other asteroid
            if (dmg >= v.maxHP)
            {

                Debug.Log(System.String.Format("{0} is ded", contact.transform.name));
                v.internalExploded = Instantiate(v.exploded, contact.transform.position, contact.transform.rotation, main.transform);
                v.internalExploded.transform.localScale = contact.transform.localScale;
                foreach (Transform t in v.internalExploded.transform)
                {
                    t.gameObject.GetComponent<shards>().force = dmg / 4;
                    t.gameObject.GetComponent<shards>().currHP = (int)contact.gameObject.transform.GetComponent<Rigidbody>().mass / 25;
                    t.gameObject.GetComponent<shards>().maxHP = (int)contact.gameObject.transform.GetComponent<Rigidbody>().mass / 25;
                    t.gameObject.GetComponent<shards>().direction = contact.transform.position;
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

                float size = contact.GetComponent<Renderer>().bounds.size.magnitude / count;

                Destroy(contact);
            }
            else
            {
                //Debug.Log("I lived, bitch");
                v.currHP -= dmg;
            }
        }
        else if (contact.tag == "Asteroid-shard")
        {
            shards v = contact.GetComponent<shards>();
            int dmg = Mathf.Max((int)collision.impulse.magnitude / 64, 1);
            if (dmg >= v.currHP)
            {

                Debug.Log(System.String.Format("{0} is ded", contact.transform.name));
                //internalExploded = Instantiate(exploded, contact.transform.position, contact.transform.rotation, main.transform);
                //internalExploded.transform.localScale = contact.transform.localScale;
                Destroy(contact);
            }

            else
            {
                //Debug.Log("I lived, bitch");
                int i = Random.Range(0, 100);
                if (i>75)
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
