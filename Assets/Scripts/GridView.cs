using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    public int numOfRows;
    public int numOfCols;
    public float horizontalSpacing;
    public float verticalSpacing;

    private float horizontalDistance = 0;
    private float verticalDistance = 0;

    public GameObject cellPrefab;
    private GameObject player;
    public GameObject playerObject;

    private List<List<GameObject>> unityCellsList = new List<List<GameObject>>();
    private GridModel gridModel;
    public Camera mainCamera;

    void Start()
    {
        player = playerObject.GetComponent<GameObject>();
        InitializeTheGrid();
    }

    void Update()
    {

    }

    void InitializeTheGrid() // Initializing the grid
    {
        gridModel = new GridModel(numOfRows, numOfCols);
        gridModel.cellDelegate += CreatingCells;
        gridModel.rowDelegate += AddRow;
        gridModel.gridAlignment += AlligningTheGrid;
        gridModel.InitializingTheCells();
    }
    public void CreatingCells(NonMonoCell cell) // Creating the cells on grid
    {
        GameObject cellView = Instantiate(cellPrefab, transform.position, cellPrefab.transform.rotation);

        unityCellsList[cell.GetRow()].Add(cellView);
        cellView.GetComponent<UnityCell>().SetCell(cell);
    }
    public void AddRow()
    {
        unityCellsList.Add(new List<GameObject>());
    }
    public void AlligningTheGrid()
    {
        float tempLength = Mathf.Sqrt(unityCellsList.Count);
        for (int x = 0; x < unityCellsList.Count; x++)
        {
            for (int y = 0; y < unityCellsList[x].Count; y++)
            {
                Vector3 tempPosition = GetPosition(x, y);
                unityCellsList[x][y].transform.position = tempPosition;
            }
            SettingTheVerticalDistance();
            SettingTheHorizontalDistance();
        }
        DefiningTheCameraPosition(FindingTheGridOriginPoint());
    }
    Vector3 GetPosition(int x, int y)
    {
        Vector3 tempVector3 = new Vector3(unityCellsList[x][y].GetComponent<UnityCell>().cellScript.GetColumn() + horizontalDistance, 0, unityCellsList[x][y].GetComponent<UnityCell>().cellScript.GetRow() + verticalDistance);
        horizontalDistance = horizontalDistance + horizontalSpacing;
        return tempVector3;
    }
    void SettingTheVerticalDistance()
    {
        verticalDistance = verticalDistance + verticalSpacing;
    }

    void SettingTheHorizontalDistance()
    {
        horizontalDistance = 0;
    }
    void DefiningTheCameraPosition(Vector3 position)
    {
        mainCamera.transform.position = position;
        mainCamera.orthographicSize = (numOfRows / 2f) + (numOfRows * verticalSpacing);
        mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, unityCellsList.Count + 1f + horizontalSpacing + verticalSpacing, mainCamera.transform.position.z);
    }

    Vector3 FindingTheGridOriginPoint()
    {
        var bounds = new Bounds(unityCellsList[0][0].transform.position, Vector3.zero);
        for (int x = 0; x < unityCellsList.Count; x++)
        {
            for (int y = 0; y < unityCellsList[x].Count; y++)
            {
                bounds.Encapsulate(unityCellsList[x][y].transform.position);
            }
        }
        return bounds.center;
    }
}
