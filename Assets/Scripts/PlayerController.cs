using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 endPosition = new Vector3(8, 5, 5);
    private Vector3 startPosition;
    private float desiredDuration = 3f;
    private float elapsedTime;

    private bool isMoving;
    public float speed;
    private Vector3 oriPos;
    private Vector3 targetPos;
    private float timeToMove = 0.15f;

    private GridView gridController;
    public GameObject gridObject;
    private GameManager gameManager;
    public GameObject gameManagerObject;

    public int numOfCols;
    public int numOfRows;
    public float gapValue;

    void Start()
    {
        StartCoroutine(PlayerSpawn());
        gameManager = gameManagerObject.GetComponent<GameManager>();
        gridController = gridObject.gameObject.GetComponent<GridView>();
        gapValue = gridController.horizontalSpacing;
        numOfCols = gridController.numOfCols - 1;
        numOfRows = gridController.numOfRows - 1;
    }

    void Update()
    {
        PlayerInput();
    }
    private void PlayerInput() // Player Input controller
    {
        if (Input.GetKeyDown(KeyCode.W) && !isMoving && (transform.position.z + gapValue <= numOfCols + numOfCols * gapValue))
        {
            StartCoroutine(PlayerMover((Vector3.forward) + new Vector3(0,0,gapValue)));
            gameManager.MusicSystem();
        }
        if (Input.GetKeyDown(KeyCode.S) && !isMoving && (transform.position.z - gapValue <= numOfCols + numOfCols * gapValue) && (transform.position.z - 1 >= 0)) // Special case for zero and greater
        {
            StartCoroutine(PlayerMover((Vector3.back) - new Vector3(0, 0, gapValue)));
            gameManager.MusicSystem();
        }
        if (Input.GetKeyDown(KeyCode.A) && !isMoving && (transform.position.x - gapValue <= numOfRows + numOfRows * gapValue) && (transform.position.x - 1 >= 0)) // Special case for zero and greater
        {
            StartCoroutine(PlayerMover((Vector3.left) - new Vector3(gapValue,0,0)));
            gameManager.MusicSystem();
        }
        if (Input.GetKeyDown(KeyCode.D) && !isMoving && (transform.position.x + gapValue <= numOfRows + numOfRows * gapValue))
        {
            StartCoroutine(PlayerMover((Vector3.right) + new Vector3(gapValue, 0, 0)));
            gameManager.MusicSystem();
        }
    }
    private IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Transform>().position = new Vector3(0, 0.5f, 0);
    }
    private IEnumerator PlayerMover(Vector3 direction) // Movement on the base of player input
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
    }
}