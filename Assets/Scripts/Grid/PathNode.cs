using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridSystem<PathNode> gridSystem;
    private GridPosition gridPosition;

    private int gCost;
    private int hCost;
    private int fCost;
    private PathNode cameFromPathNode;
    public PathNode(GridSystem<PathNode> gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost/* + ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(gridPosition).GetGridTileVisual().GetWalkCost()*/;
    }
    public int GetHCost()
    {
        return hCost;
    }
    public int GetFCost()
    {
        return fCost;
    }

    public void SetGCost(int gCost)
    {
        this.gCost = gCost;
    }
    public void SetHCost(int hCost)
    {
        this.hCost = hCost;
    }
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public PathNode GetCameFromPathNode()
    {
        return this.cameFromPathNode;
    }
    public void SetCameFromPathNode(PathNode pathNode)
    {
        this.cameFromPathNode = pathNode;
    }
    public void ResetCameFromPathNode()
    {
        cameFromPathNode = null;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
