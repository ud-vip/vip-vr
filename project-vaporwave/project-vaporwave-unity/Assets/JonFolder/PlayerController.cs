using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject dartPrefab;
    public Transform dartSpawn;

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Moving
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        //Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var dart = (GameObject)Instantiate(
            dartPrefab,
            dartSpawn.position,
            dartSpawn.rotation);

        // Add velocity to the bullet
        dart.GetComponent<Rigidbody>().velocity = dart.transform.up * 6;

        //Spawn for clients too
        NetworkServer.Spawn(dart);

        // Destroy the bullet after 2 seconds
        Destroy(dart, 2.0f);
    }
}