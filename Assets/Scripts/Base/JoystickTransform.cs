using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickTransform : MonoBehaviour
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
        rb.isKinematic = true;
    }

    public void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
            {
                animator.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0f, (Mathf.Atan2(horizontal * 180, vertical * 180) * Mathf.Rad2Deg), 0f);
                transform.position += MoveSpeed * Time.deltaTime * transform.forward;
            }
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}
