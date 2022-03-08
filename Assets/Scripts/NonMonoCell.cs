using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMonoCell
{
    private int row;
    private int col;
    public enum Status { none, Preserved }
    public Status status;

    public delegate void OnStatusUpdate(Status status);
    public event OnStatusUpdate onStatusUpdate;



    void InitializeCell()
    {
        this.status = Status.none;
        row = 0;
        col = 0;
    }
    public NonMonoCell()
    {
        InitializeCell();
    }

    public NonMonoCell(int row, int col)
    {
        this.status = Status.none;
        this.row = row;
        this.col = col;
    }

    public int GetRow()
    {
        return row;
    }

    public int GetColumn()
    {
        return col;
    }

    public void SetStatus(Status tempStatus)
    {
        this.status = tempStatus;
        onStatusUpdate.Invoke(tempStatus);
    }
    public void ChangeTheState()
    {
        if (this.status == Status.none)
        {
            SetStatus(Status.Preserved);
        }
        Debug.Log("State Chnaged");
    }

    public Status GetStatus()
    {
        return status;
    }

}
