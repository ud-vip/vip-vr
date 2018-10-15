using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class skybox : MonoBehaviour
{
    GameObject skyboxCube;
    // Use this for initialization
    void Start()
    {
        skyboxCube = transform.gameObject;
        RemoveExistingColliders();
        InvertMesh();
        gameObject.AddComponent<MeshCollider>();

    }
    private void RemoveExistingColliders()
    {
        Collider[] colliders = GetComponents<Collider>();
        for (int i = 0; i < colliders.Length; i++)
            DestroyImmediate(colliders[i]);
    }
    private void InvertMesh()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
    void OnCollisionEnter(Collision collision)
    {   
        Debug.Log("You have left the playing field, by");
        Destroy(collision.gameObject);
    }
}
