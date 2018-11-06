using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vars : MonoBehaviour
{


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
    
    // Got hit by another object
    // Format of tmp is [tag,dmg]
    // tag is a string, dmg is a float
    void takeDMG(object[] tmp)
    {
        string tag = (string)tmp[0];
        int dmg = (int)(float)tmp[1];
        // Modify dmg stat
        if (tag == "Asteroid")
        {
            dmg = dmg;
        }
        else if (tag == "Asteroid-shard")
        {
            dmg = dmg;
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
        if (dmg >= maxHP)
        {

            Debug.Log(System.String.Format("{0} is ded", transform.name));
            internalExploded = Instantiate(exploded, transform.position, transform.rotation, main.transform);
            internalExploded.transform.localScale = transform.localScale;
            foreach (Transform t in internalExploded.transform)
            {
                t.gameObject.GetComponent<shards>().force = dmg / 16;
                t.gameObject.GetComponent<shards>().currHP = (int)gameObject.transform.GetComponent<Rigidbody>().mass / 12;
                t.gameObject.GetComponent<shards>().maxHP = (int)gameObject.transform.GetComponent<Rigidbody>().mass / 12;
                t.gameObject.GetComponent<shards>().direction = transform.GetComponent<Rigidbody>().velocity;
                t.gameObject.GetComponent<Rigidbody>().mass = gameObject.transform.GetComponent<Rigidbody>().mass / 25;
                t.SendMessage("explode",transform.position);
            }
            //Explode(dmg/100f);
            Destroy(gameObject);

        }
        else if (dmg >= currHP)
        {
            Debug.Log("live, my children");
            int count = (int)Random.Range(1, 5);

            float size = GetComponent<Renderer>().bounds.size.magnitude / count;

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("I lived, bitch");
            currHP -= dmg;
        }
    }


    public static float KineticEnergy(Rigidbody rb)
    {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject contact = collision.gameObject;
        object[] tmp = new object[2];
        // For an asteroid, dmg done is based on its kinetic energy.
        // For weapons, this can be replaced with a weapons dmg
        // tmp will always be the tag of the object that caused the dmg and how much dmg
        tmp[0] = gameObject.tag;
        tmp[1] = KineticEnergy(rb);
        Debug.Log(System.String.Format("Collision between {0} with {1}", go.name, contact.name));
        contact.SendMessage("takeDMG", tmp, SendMessageOptions.DontRequireReceiver);
    }
}
