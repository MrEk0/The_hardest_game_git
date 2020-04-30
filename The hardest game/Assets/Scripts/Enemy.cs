using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Vertical,
    Horizontal
}

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] MovementType movementType;
    [SerializeField] float speed=5f;

    Rigidbody rb;
    Transform myTransform;

    float gameAreaLength;
    float gameAreaHeight;
    float localScale;
    float minPosX;
    float maxPosX;
    float minPosZ;
    float maxPosZ;
    Vector3 target;

    public Transform GameArea { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

        localScale = transform.localScale.x;
        gameAreaLength = GameArea.GetComponent<MeshRenderer>().bounds.size.x * 0.5f;
        gameAreaHeight = GameArea.GetComponent<MeshRenderer>().bounds.size.z * 0.5f;
        maxPosX = gameAreaLength - localScale;
        minPosX = -maxPosX;
        maxPosZ = gameAreaHeight - localScale;
        minPosZ = -maxPosZ;

        if (movementType == MovementType.Horizontal)
        {
            target = new Vector3(maxPosX, myTransform.position.y, myTransform.position.z);
        }
        else
        {
            target = new Vector3(myTransform.position.x, myTransform.position.y, maxPosZ);
        }
        Debug.Log(target);
    }

    private void Update()
    {
        if (enemyController.isGameOver)
            return;

        float distance = Vector3.SqrMagnitude(target - myTransform.position);

        if (Mathf.Approximately(0, distance))
        {
            if (movementType == MovementType.Horizontal)
            {
                target.x = target.x == maxPosX ? minPosX : maxPosX;
            }
            else
            {
                target.z = target.z == maxPosZ ? minPosZ : maxPosZ;
            }
            Debug.Log(target);
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
