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
    public void explode(Vector3 point)
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
            float f = Random.Range((int)(force / 2), force * 2);
            
            rb.AddExplosionForce(Random.Range(force / 2, force * 2), point, 2);
            rb.AddForce(direction, ForceMode.Impulse);
            //rb.AddExplosionForce(300, shard.transform.position, 20);
            rb.mass = rb.mass / shard.GetComponent<Renderer>().bounds.size.magnitude;
        }
        else
        {
            Debug.Log("No rb");
            // No RB, cant affect it, kill it.
            //Destroy(transform.gameObject);
        }
    }
    void takeDMG(object[] tmp)
    {
        string tag = (string)tmp[0];
        int dmg = (int)(float)tmp[1];
        // Modify dmg stat
        if (tag == "Asteroid")
        {
            dmg = dmg/8;
        }
        else if (tag == "Asteroid-shard")
        {
            dmg = Mathf.Min(dmg/32,1);
        }
        else if (tag == "skybox")
        {
            dmg = maxHP;
        }
        else
        {
            // No idea how you would get here, but die anyway to save the others
            Debug.Log("How!?!?!?!?!?");
            Destroy(gameObject);
        }
        // Took to much dmg, explode
        if (dmg >= maxHP || dmg>=currHP)
        {

            Debug.Log(System.String.Format("{0} is ded", transform.name));
            Destroy(gameObject);

        }
        else
        {
            //Debug.Log("I lived, bitch");
            currHP -= dmg;
        }
    }
    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
    public void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;
        object[] tmp = new object[2];
        // For an asteroid, dmg done is based on its kinetic energy.
        // For weapons, this can be replaced with a weapons dmg
        // tmp will always be the tag of the object that caused the dmg and how much dmg
        tmp[0] = gameObject.tag;
        tmp[1] = KineticEnergy(shard.GetComponent<Rigidbody>());
        contact.SendMessage("takeDMG", tmp, SendMessageOptions.DontRequireReceiver);
    }
    
}
