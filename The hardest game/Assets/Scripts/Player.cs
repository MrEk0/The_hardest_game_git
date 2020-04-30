using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 8f;

    Rigidbody rb;
    float yMovement;
    float xMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xMovement= Input.GetAxisRaw("Horizontal");
        yMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(xMovement * speed, 0f, yMovement * speed);
    }
}
