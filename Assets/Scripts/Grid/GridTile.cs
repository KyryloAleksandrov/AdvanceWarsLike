using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{   
    private GridSystem<GridTile> gridSystem;
    private GridPosition gridPosition;

    public GridTile(GridSystem<GridTile> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
