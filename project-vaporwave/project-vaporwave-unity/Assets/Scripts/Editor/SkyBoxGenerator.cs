using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;
using Array = UnityScript.Lang.Array;

/**
 * C# port of http://wiki.unity3d.com/index.php/Skybox_Generator
 */

public class SkyBoxGenerator : ScriptableWizard
{
    public Transform renderFromPosition;

    public String[] skyBoxImage =
    {
        "frontImage", "rightImage",
        "backImage", "leftImage",
        "upImage", "downImage"
    };

    public Vector3[] skyDirection =
    {
        new Vector3(0, 0, 0), new Vector3(0, -90, 0),
        new Vector3(0, 180, 0), new Vector3(0, 90, 0),
        new Vector3(-90, 0, 0), new Vector3(90, 0, 0)
    };

    void OnWizardUpdate()
    {
        var helpString = "Select transform to render from";
        var isValid = (renderFromPosition != null);
    }

    void OnWizardCreate()
    {
        var go = new GameObject("SkyboxCamera");
        go.AddComponent<Camera>();
        go.GetComponent<Camera>().backgroundColor = Color.black;
        go.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        go.GetComponent<Camera>().fieldOfView = 90;
        go.GetComponent<Camera>().aspect = 1.0f;

        go.transform.position = renderFromPosition.position;

        if (renderFromPosition.GetComponent<Renderer>())
        {
            go.transform.position = renderFromPosition.GetComponent<Renderer>().bounds.center;
        }

        go.transform.rotation = Quaternion.identity;

        for (var orientation = 0; orientation < skyDirection.Length; orientation++)
        {
            renderSkyImage(orientation, go);
        }

        DestroyImmediate(go);
    }


    [MenuItem("Custom/Render Skybox", false, 4)]
    static void CreateWizard()
    {
        DisplayWizard<SkyBoxGenerator>("Render Skybox", "Render!");
    }

    void renderSkyImage(int orientation, GameObject go)
    {
        go.transform.eulerAngles = skyDirection[orientation];
        var screenSize = 1024;
        var rt = new RenderTexture(screenSize, screenSize, 24);
        go.GetComponent<Camera>().targetTexture = rt;
        var screenShot = new Texture2D(screenSize, screenSize, TextureFormat.RGB24, false);
        go.GetComponent<Camera>().Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenSize, screenSize), 0, 0);

        RenderTexture.active = null;
        DestroyImmediate(rt);

        var bytes = screenShot.EncodeToPNG();

        var directory = "Assets/Skyboxes";

        if (!System.IO.Directory.Exists(directory))
        {
            System.IO.Directory.CreateDirectory(directory);
        }

        System.IO.File.WriteAllBytes(System.IO.Path.Combine(directory, skyBoxImage[orientation] + ".png"), bytes);
    }
}