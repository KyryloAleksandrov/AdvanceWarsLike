using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<TGridObject>
{
    private int width;
    private int height;
    private float cellSize;

    private TGridObject[,] gridObjectArray;


    
    public GridSystem(int width, int height, float cellSize, Func<GridSystem<TGridObject>, GridPosition, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new TGridObject[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = createGridObject(this, gridPosition);
                //Debug.Log(gridObjectArray[x,z]);
            }
        }
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public TGridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public TGridObject[,] getGridObjectArray()
    {
        return gridObjectArray;
    }

    public bool IsInBounds(GridPosition gridPosition)
    {
        if(gridPosition.x >= 0 && gridPosition.x < width && gridPosition.z >= 0 && gridPosition.z < height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisplayCoordinates(Transform coordinatesPrefab)
    {
        for (int x = 0; x < width; x++){
            for (int z = 0; z < height; z++){
                GridPosition gridPosition = new GridPosition(x, z);
                Transform coordinates = GameObject.Instantiate(coordinatesPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                GridDebugObject grodDebugObject = coordinates.GetComponent<GridDebugObject>();
                grodDebugObject.SetGridObject(GetGridObject(gridPosition)); 
            }
        }
    }

    public List<GridPosition> GetNeighbourPositions(GridPosition gridPosition)
    {
        List<GridPosition> neighbourPositions = new List<GridPosition>();

        GridPosition positionUp = new GridPosition(gridPosition.x + 0, gridPosition.z + 1);
        if(IsInBounds(positionUp))
        {
            neighbourPositions.Add(positionUp);
        }

        GridPosition positionDown = new GridPosition(gridPosition.x + 0, gridPosition.z - 1);
        if(IsInBounds(positionDown))
        {
            neighbourPositions.Add(positionDown);
        }

        GridPosition positionLeft = new GridPosition(gridPosition.x - 1, gridPosition.z + 0);
        if(IsInBounds(positionLeft))
        {
            neighbourPositions.Add(positionLeft);
        }

        GridPosition positionRight = new GridPosition(gridPosition.x + 1, gridPosition.z + 0);
        if(IsInBounds(positionRight))
        {
            neighbourPositions.Add(positionRight);
        }

        return neighbourPositions;
    }
}
