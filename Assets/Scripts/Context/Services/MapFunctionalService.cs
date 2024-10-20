using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMapFunctionalService  //TODO - rename to MapGridSetupService
{
    GridSystem<GridObject> gridSystem {get;}
    Transform cellOutlinePrefab {get;}
    Transform coordinatesPrefab {get;}
    

    void InitializeGrid();
    GridSystem<T> CreateGridSystem<T>(Func<GridSystem<T>, GridPosition, T> createGridObject);
}

public class MapFunctionalService : IMapFunctionalService
{
    public GridSystem<GridObject> gridSystem { get; }
    public Transform cellOutlinePrefab { get; }
    public Transform coordinatesPrefab { get; }

    public int width;
    public int height;
    public float cellSize;

    public MapFunctionalService(IConfigService ConfigService)
    {
        var MapData = ConfigService.MapData;

        width = MapData.width;
        height = MapData.height;
        cellSize = MapData.cellSize;

        gridSystem = new GridSystem<GridObject>(width, height, cellSize, (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
        cellOutlinePrefab = ConfigService.MapData.cellOutlinePrefab;
        coordinatesPrefab = ConfigService.MapData.coordinatesPrefab;
    }

    public void InitializeGrid()
    {
        Debug.Log("Initializing Grid");
        GridObject[,] gridObjectArray = gridSystem.getGridObjectArray();

        for (int x = 0; x < gridObjectArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjectArray.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjectArray[x, z].GetGridPosition();
                Transform cellOutlineTransform = GameObject.Instantiate(cellOutlinePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

                GridOutline gridOutline = cellOutlineTransform.GetComponent<GridOutline>();
                GridObject gridObject = gridSystem.GetGridObject(gridPosition);
                
                gridOutline.SetGridObject(gridObject);
                gridObject.SetGridOutline(gridOutline);
            }
        }

        //gridSystem.DisplayCoordinates(coordinatesPrefab);
    }
    public GridSystem<T> CreateGridSystem<T>(Func<GridSystem<T>, GridPosition, T> createGridObject)
    {
        return new GridSystem<T>(width, height, cellSize, createGridObject);
    }
}
