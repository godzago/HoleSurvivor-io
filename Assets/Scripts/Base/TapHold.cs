using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHold : MonoBehaviour
{
    private float startTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - startTime < 0.15f)
            {
                Tap();
            }
            else
            {
                Hold();
            }
        }
    }

    void Tap()
    {
        Debug.Log("Tap");
    }

    void Hold()
    {
        Debug.Log("Hold");
    }
}
