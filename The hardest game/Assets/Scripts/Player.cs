using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] Joystick joystick;
    [SerializeField] float joystickMinOffset = 0.2f;

    Rigidbody rb;
    float zMovement;
    float xMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        XAxisMovement();

        ZAxisMovement();
    }

    private void XAxisMovement()
    {
        if (joystick.Horizontal >= joystickMinOffset)
        {
            xMovement = speed;
        }
        else if (joystick.Horizontal <= -joystickMinOffset)
        {
            xMovement = -speed;
        }
        else
        {
            xMovement = 0f;
        }
    }

    private void ZAxisMovement()
    {
        if (joystick.Vertical >= joystickMinOffset)
        {
            zMovement = speed;
        }
        else if (joystick.Vertical <= -joystickMinOffset)
        {
            zMovement = -speed;
        }
        else
        {
            zMovement = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xMovement, 0f, zMovement);
    }
}
