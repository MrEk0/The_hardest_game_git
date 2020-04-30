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

    private Rigidbody rb;
    private Transform myTransform;
    private Vector3 target;

    private float gameAreaLength;
    private float gameAreaHeight;
    private float localScale;
    private float posX;
    private float posZ;

    public Transform GameArea { get; set; }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

        StartInitialization();
    }

    private void StartInitialization()
    {
        localScale = transform.localScale.x;
        gameAreaLength = GameArea.GetComponent<MeshRenderer>().bounds.size.x * 0.5f;
        gameAreaHeight = GameArea.GetComponent<MeshRenderer>().bounds.size.z * 0.5f;
        posX = gameAreaLength - localScale;
        posZ = gameAreaHeight - localScale;

        if (movementType == MovementType.Horizontal)
        {
            target = new Vector3(posX, myTransform.position.y, myTransform.position.z);
        }
        else
        {
            target = new Vector3(myTransform.position.x, myTransform.position.y, posZ);
        }
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
                target.x = target.x == posX ? -posX : posX;
            }
            else
            {
                target.z = target.z == posZ ? -posZ : posZ;
            }
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
