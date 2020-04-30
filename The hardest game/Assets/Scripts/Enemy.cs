using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] float speed=5f;

    Rigidbody rb;
    Transform myTransform;

    float gameAreaLength;
    float localScale;
    float minPos;
    float maxPos;
    Vector3 target;

    public Transform GameArea { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

        localScale = transform.localScale.x;
        gameAreaLength = GameArea.GetComponent<MeshRenderer>().bounds.size.x * 0.5f;
        maxPos = gameAreaLength - localScale;
        minPos = -maxPos;

        target = new Vector3(maxPos, myTransform.position.y, myTransform.position.z);
    }

    private void Update()
    {
        if (enemyController.isGameOver)
            return;

        float distance = Vector2.SqrMagnitude(target - myTransform.position);

        if (Mathf.Approximately(0, distance))
        {
            target.x = target.x == maxPos ? minPos : maxPos;
        }
    }

    private void FixedUpdate()
    {
        if (enemyController.isGameOver)
            return;

        Vector3 newPos = Vector3.MoveTowards(myTransform.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            enemyController.ShowLosePanel();
        }
    }
}
