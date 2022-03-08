using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCell : MonoBehaviour
{
    public List<GameObject> cellOptions;
    private NonMonoCell.Status activeStatus;
    public NonMonoCell cellScript = new NonMonoCell();

    void Start()
    {
        cellScript.onStatusUpdate += SetState;
    }

    public void SetState(NonMonoCell.Status status)
    {
        for (int i = 0; i < cellOptions.Count; i++)
        {
            if ((int)status == i)
            {
                cellOptions[i].SetActive(true);
            }
            else
            {
                cellOptions[i].SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        cellScript.onStatusUpdate -= SetState;
    }

    public void SetCell(NonMonoCell cellScript)
    {
        this.cellScript = cellScript;
        SetState(cellScript.GetStatus());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Collided");
            cellScript.ChangeTheState();
        }
    }
}