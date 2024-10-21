using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile
{   
    private GridSystem<GridTile> gridSystem;
    private GridPosition gridPosition;

    private GridTileVisual gridTileVisual;

    public GridTile(GridSystem<GridTile> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public GridTileVisual GetGridTileVisual()
    {
        return gridTileVisual;
    }
    public void SetGridTileVisual(GridTileVisual gridTileVisual)
    {
        this.gridTileVisual = gridTileVisual;
    }

    

    
}
