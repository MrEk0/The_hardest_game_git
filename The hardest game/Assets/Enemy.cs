using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform gameArea;
    [SerializeField] float speed=5f;

    Rigidbody2D rb;
    Transform myTransform;

    float gameAreaLength;
    float localScale;
    float minPos;
    float maxPos;
    Vector3 target;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();

        localScale = transform.localScale.x*0.5f;
        gameAreaLength = gameArea.localScale.x * 0.5f;
        maxPos = gameAreaLength - localScale;
        minPos = -maxPos;

        target = new Vector3(maxPos, myTransform.position.y, myTransform.position.z);
    }

    private void Update()
    {
        float distance = Vector2.SqrMagnitude(target - myTransform.position);

        if (Mathf.Approximately(0, distance))
        {
            target.x = target.x == maxPos ? minPos : maxPos;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(myTransform.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
}
