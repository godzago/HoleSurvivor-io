using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveHorizontal : MonoBehaviour
{
    private float Horizontal;

    [SerializeField] private float Speed = 1;
    [SerializeField] private float ClampValue = 3;

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Horizontal += Speed * SimpleInput.GetAxis("Mouse X");

            Horizontal = Mathf.Clamp(Horizontal, -ClampValue, ClampValue);

            transform.localPosition = new Vector3(Horizontal, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
