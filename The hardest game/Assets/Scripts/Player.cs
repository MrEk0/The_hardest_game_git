using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] Joystick joystick;

    Rigidbody rb;
    float zMovement;
    float xMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(joystick.Horizontal>=.2f)
        {
            xMovement = speed;
        }
        else if(joystick.Horizontal<=-.2f)
        {
            xMovement = -speed;
        }
        else
        {
            xMovement = 0f;
        }

        if (joystick.Vertical >= .2f)
        {
            zMovement = speed;
        }
        else if (joystick.Vertical <= -.2f)
        {
            zMovement = -speed;
        }
        else
        {
            zMovement = 0f;
        }
        //xMovement = Input.GetAxisRaw("Horizontal");
        //zMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xMovement, 0f, zMovement);
    }
}
