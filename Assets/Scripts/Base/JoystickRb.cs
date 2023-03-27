using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickRb : MonoBehaviour
{
    private Joystick joystick;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private float MoveSpeed = 10f;

    private void Awake()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
            {
                animator.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0f, (Mathf.Atan2(horizontal * 180, vertical * 180) * Mathf.Rad2Deg), 0f);
                rb.velocity = MoveSpeed * Time.fixedDeltaTime * transform.forward;
            }
        }
        else
        {
            animator.SetBool("run", false);
            rb.velocity = Vector3.zero;
        }
    }
}
