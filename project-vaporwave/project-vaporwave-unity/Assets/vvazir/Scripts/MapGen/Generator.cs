using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Range(0f, 1f)]
    public float threshold = .8f;
    [Range(100, 1000)]
    public int resolution = 100;
    [Range(0f, 1f)]
    public float chunkSize = .2f;
    public float largeAsteroidSize = 2f;
    public Vector3 offset;
    public Vector3 rotation;

    public float frequency = 1f;

    [Range(0f, 1f)]
    public float strength = 1f;

    public bool damping;
    [Range(1, 8)]
    public int octaves = 1;

    [Range(1f, 4f)]
    public float lacunarity = 2f;

    [Range(0f, 1f)]
    public float persistence = 0.5f;

    [Range(1, 3)]
    public int dimensions = 3;

    public NoiseMethodType type;

    public int asteroidLimit = 10;

    private int currentResolution;

    private GameObject[] asteroids;
    private int count = 0;
    private GameObject go;
    private Texture2D texture;
    private GameObject exploded;
    private GameObject skybox;
    
    public GameObject getAsteroid(int i)
    {
        if (i >= asteroids.Length || i <0)
        {
            return asteroids[1];
        }
        else
        {
            return asteroids[i];
        }
    }
    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 120;
        go = this.gameObject;
        float scale = resolution + Mathf.Log10(resolution) * 50;
        skybox = transform.Find("skybox").gameObject;
        skybox.transform.position = transform.position + new Vector3(scale/8,scale/8,scale/8);
        skybox.transform.localScale = Vector3.one * (scale);
        Debug.Log(skybox.transform.localScale);
        asteroids = Resources.LoadAll<GameObject>("Asteroids/Prefabs/Asteroids");
        exploded = Resources.Load<GameObject>("Asteroids/Prefabs/Asteroids-fractured-fill");
        CreateAsteroids();
        MergeAsteroids();
        Debug.Log(go.transform.childCount);
    }
    private void OnEnable()
    {
        /*
        if (texture == null)
        {
            texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true)
            {
                name = "Procedural Texture",
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Trilinear,
                anisoLevel = 9
            };
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
        */
    }
    public void MergeAsteroids()
    {
        foreach (Transform checkingChild in go.transform)
        {
            if (checkingChild.tag != "Asteroid")
                continue;
            if (checkingChild.gameObject != null && checkingChild.parent!=null)
            {
                MeshCollider mc = checkingChild.GetComponent<MeshCollider>();
                foreach (Transform checkChild in go.transform)
                {
                    if (checkChild.tag != "Asteroid")
                        continue;
                    if (checkChild.gameObject != null && checkChild.parent!=null && checkingChild.name != checkChild.name)
                    {
                        MeshCollider tmpMC = checkChild.GetComponent<MeshCollider>();
                        if (mc.bounds.Intersects(tmpMC.bounds))
                        {
                            
                            //Debug.Log(System.String.Format("Merging {0} with {1}",checkingChild.name,checkChild.name));
                            //Debug.Log(checkingChild.transform.localScale.magnitude);
                            checkingChild.transform.localScale += Vector3.one * (tmpMC.transform.localScale.x * 2);
                            checkingChild.gameObject.GetComponent<vars>().maxHP += checkChild.gameObject.GetComponent<vars>().maxHP;
                            checkingChild.gameObject.GetComponent<vars>().currHP += checkChild.gameObject.GetComponent<vars>().currHP;

                            //Debug.Log(checkingChild.transform.localScale.magnitude);
                            checkChild.parent = null;
                            GameObject.Destroy(checkChild.gameObject);
                            //Debug.Log(checkChild.gameObject == null);
                            count--;
                        }
                    }
                }
            }
        }
        
    }
    public void CreateAsteroids()
    {
        foreach (Transform child in go.transform)
        {
            if (child.tag == "Asteroid")
            {
                child.parent = null;
                GameObject.Destroy(child.gameObject);
                count--;
            }
        }
        count = 0;
        float stepSize = 1f / resolution;
        NoiseMethod method = Noise.noiseMethods[(int)type][dimensions - 1];
        int scale = Mathf.FloorToInt(chunkSize * resolution);
        long scale3 = (long)Mathf.Pow(scale, 3f);        
        if (chunkSize != 0f)
        {
            Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
            Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
            Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
            Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));
            Vector3 zpoint0 = transform.TransformPoint(new Vector3(0, 0, -0.5f));
            Vector3 zpoint1 = transform.TransformPoint(new Vector3(0, 0, 0.5f));
            float sum = 0f;
            //long pos = (long)Mathf.Pow(resolution, 3f);
            //for (long currPos = 0; currPos < pos; currPos += scale3)
            //{

            //    long z = currPos % resolution;
            //    long x = currPos % (long)Mathf.Pow(z, 2f);
            //    long y = currPos % (long)Mathf.Pow(x, 3f);
            //}
            for (int y = 0; y < resolution; y += 1)
            {
                int yi = y % scale;
                Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
                Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
                for (int x = 0; x < resolution; x += 1)
                {
                    int xi = x % scale;
                    Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                    for (int z = 0; z < resolution; z += 1)
                    {
                        int zi = z % scale;
                        Vector3 zpoint = Vector3.Lerp(zpoint0, zpoint1, (z + 0.5f) * stepSize);
                        point += zpoint;

                        point.z = (z + 0.5f) * stepSize;
                        float sample = Noise.Sum(method, point * 1f, frequency, octaves, lacunarity, persistence).value;
                        if (type != NoiseMethodType.Value)
                        {
                            sample = sample * 0.5f + 0.5f;
                        }
                        sum += sample;

                        if ((sum > (threshold * (scale * scale * scale)) * largeAsteroidSize) || (yi == scale / 2 && xi == scale / 2 && zi == scale / 2))
                        {
                            if (sum > threshold * (scale * scale * scale) && count < asteroidLimit)
                            {
                                //Debug.Log(sum);
                                count++;
                                int idx = (Random.Range(0, asteroids.Length));
                                GameObject tmp = Instantiate(
                                    asteroids[idx],                            // Random Asteroid
                                    new Vector3((x) * 1f,                          // Random Location
                                                (y) * 1f,
                                                (z) * 1f),
                                    Quaternion.identity,transform);                                           // Same rotation

                                tmp.tag = "Asteroid";
                                tmp.AddComponent<vars>();
                                tmp.name = tmp.name.Replace("(Clone)", (System.String.Format("({0})", count))).Trim();
                                vars v = tmp.GetComponent<vars>();
                                // Info
                                v.idx = idx;
                                v.go = tmp;
                                v.main = transform.gameObject;
                                v.exploded = exploded;
                                // Transform
                                tmp.transform.parent = go.transform;                                // Set as child of main
                                tmp.transform.localScale *= (sum) * (1f / ((scale * scale * scale)));                        // Scale based on position
                                // HP
                                v.maxHP =(int)((sum) * (1f / ((scale * scale * scale))) * 1000);
                                v.currHP = tmp.GetComponent<vars>().maxHP;
                                // Mesh collider
                                v.mc = tmp.AddComponent<MeshCollider>();
                                v.mc.convex = true;
                                // Rigidbody
                                v.rb = tmp.AddComponent<Rigidbody>();
                                v.rb.isKinematic = false;
                                v.rb.angularVelocity = Random.insideUnitSphere * Random.Range(.01f, 1f);
                                v.rb.drag = 0;
                                v.rb.angularDrag = 0;
                                v.rb.useGravity = false;
                                v.rb.mass = v.maxHP;
                                


                            }
                            sum = 0f;
                        }

                        if (count >= asteroidLimit)
                        {
                            break;
                        }
                    }
                    if (count >= asteroidLimit)
                    {
                        break;
                    }
                }
                if (count >= asteroidLimit)
                {
                    break;
                }
            }
        }
        
        //texture.Apply();

    }

    // Update is called once per frame
    //void Update () {
            
    //}
}
