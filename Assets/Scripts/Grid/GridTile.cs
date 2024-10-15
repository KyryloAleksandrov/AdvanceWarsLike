using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{   
    private GridPosition gridPosition;

    public GridTile(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
