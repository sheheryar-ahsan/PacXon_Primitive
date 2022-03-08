using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModel : Matrix
{
    List<List<NonMonoCell>> cellGrid = new List<List<NonMonoCell>>();
    NonMonoCell.Status currentTurn = NonMonoCell.Status.none;

    public delegate void CellDelegate(NonMonoCell cell);
    public event CellDelegate cellDelegate;

    public delegate void RowDelegate();
    public event RowDelegate rowDelegate;

    public delegate void GridAlignment();
    public event GridAlignment gridAlignment;

    public delegate void StateChange();
    public StateChange stateChange;

    private NonMonoCell nonMonoCell = new NonMonoCell();

    public GridModel(int rows, int cols) : base(rows, cols)
    {

    }
    public GridModel()
    {
        stateChange += ChangeTheState;
    }
    public void InitializingTheCells()
    {
        for (int x = 0; x < GetNoOfRows(); x++)
        {
            cellGrid.Add(new List<NonMonoCell>());
            rowDelegate?.Invoke();
            for (int y = 0; y < GetNoOfColumns(); y++)
            {
                NonMonoCell cell = new NonMonoCell(x, y);
                //stateChange += ChangeTheState;
                cellGrid[x].Add(cell);
                cellDelegate?.Invoke(cell);
            }
        }
        gridAlignment?.Invoke();
    }
    public override void OnMatrixUpdate()
    {
        for (int x = 0; x < GetNoOfRows(); x++)
        {
            for (int y = 0; y < GetNoOfColumns(); y++)
            {
                cellGrid[x][y].SetStatus((NonMonoCell.Status)GetValue(x, y));
            }
        }
    }
    void ChangeTheState()
    {
        Debug.Log("Changing the State");
        if (currentTurn == NonMonoCell.Status.none)
        {
            currentTurn = NonMonoCell.Status.Preserved;
        }
    }
}
