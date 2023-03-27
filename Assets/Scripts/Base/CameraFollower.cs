using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float lerpSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new(0, -5, 5);
    [SerializeField] private bool CanLookAt = false;
    Vector3 lerpPos;
    Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() => CameraMove();

    void CameraMove()
    {
        if (target == null) return;

        lerpPos = Vector3.Lerp(transform.localPosition, target.localPosition - offset, lerpSpeed);
        transform.localPosition = lerpPos;

        if (CanLookAt)
            transform.LookAt(target.localPosition);
    }
}
