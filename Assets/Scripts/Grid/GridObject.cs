using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem<GridObject> gridSystem;
    private GridPosition gridPosition;
    private GridOutline gridOutline;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public GridOutline GetGridOutline()
    {
        return gridOutline;
    }

    public void SetGridOutline(GridOutline gridOutline)
    {
        this.gridOutline = gridOutline;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    
}
