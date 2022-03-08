using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool isMoving;
    public float speed;
    private Vector3 oriPos;
    private Vector3 targetPos;
    private float timeToMove = 0.15f;

    public int numOfCols;
    public int numOfRows;
    public float gapValue;

    private GridView gridController;
    public GameObject gridObject;

    private GameObject player;
    private Rigidbody enemyRB;
    public float speedOfEnemy;

    void Start()
    {
        gridController = gridObject.gameObject.GetComponent<GridView>();
        isMoving = false;

        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!isMoving)
        {
            FollowEnemy();
        }
    }
    private void FollowEnemy()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRB.AddForce(lookDirection * speed);
    }
    private void EnemyInput() // Enemy Input controller
    {
        int selection = Random.Range(0,4);
        if (selection == 0 && (transform.position.z + gapValue <= numOfCols + numOfCols * gapValue))
        {
            StartCoroutine(EnemyMover((Vector3.forward) + new Vector3(0, 0, gapValue)));
            Debug.Log("Moving Forward");
        }
        if (selection == 1 && (transform.position.z - gapValue <= numOfCols + numOfCols * gapValue) && (transform.position.z - 1 >= 0)) // Special case for zero and greater
        {
            StartCoroutine(EnemyMover((Vector3.back) - new Vector3(0, 0, gapValue)));
            Debug.Log("Moving Back");
        }
        if (selection == 2 && (transform.position.x - gapValue <= numOfRows + numOfRows * gapValue) && (transform.position.x - 1 >= 0)) // Special case for zero and greater
        {
            StartCoroutine(EnemyMover((Vector3.left) - new Vector3(gapValue, 0, 0)));
            Debug.Log("Moving Left");
        }
        if (selection == 3 && (transform.position.x + gapValue <= numOfRows + numOfRows * gapValue))
        {
            StartCoroutine(EnemyMover((Vector3.right) + new Vector3(gapValue, 0, 0)));
            Debug.Log("Moving Right");
        }
        return;
    }
    private IEnumerator EnemyMover(Vector3 direction) // Movement of enemy from one point to another
    {
        isMoving = true;
        float elapsedTime = 0;

        oriPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPos = oriPos + direction;

        while (elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(oriPos, targetPos, (elapsedTime / timeToMove));
            yield return null;
        }

        isMoving = false;
    }
}
