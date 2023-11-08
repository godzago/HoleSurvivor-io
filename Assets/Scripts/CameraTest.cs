using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public GameObject CAMERA1;
    public GameObject camera2;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CAMERA1.SetActive(true);
            camera2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CAMERA1.SetActive(false);
            camera2.SetActive(true);
        }
    }
}
