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
        Application.targetFrameRate = 60;
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
                //animator.SetBool("run", true);
                //transform.rotation = Quaternion.Euler(0f, (Mathf.Atan2(horizontal * 180, vertical * 180) * Mathf.Rad2Deg), 0f);
                //rb.velocity = MoveSpeed * Time.fixedDeltaTime * transform.forward;

                animator.SetBool("run", true);
                transform.rotation = Quaternion.Euler(0f, (Mathf.Atan2(horizontal * 180, vertical * 180) * Mathf.Rad2Deg), 0f);

                // Sýnýrlama ekleme
                Vector3 newPosition = transform.position + (MoveSpeed * Time.fixedDeltaTime * transform.forward);
                newPosition.x = Mathf.Clamp(newPosition.x, -6, 6); // X sýnýrlama
                newPosition.z = Mathf.Clamp(newPosition.z, -6.5f, 6.5f);// Z sýnýrlama
                transform.position = newPosition;

            }
        }
        else
        {
            animator.SetBool("run", false);
            rb.velocity = Vector3.zero;
        }
    }
}
