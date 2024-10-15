using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapFunctionalService
{
    GridSystem gridSystem {get;}
    Transform cellOutlinePrefab {get;}
    Transform coordinatesPrefab {get;}

    void InitialzieGrid();
}

public class MapFunctionalService : IMapFunctionalService
{
    public GridSystem gridSystem {get;}
    public Transform cellOutlinePrefab {get;}
    public Transform coordinatesPrefab {get;}

    public MapFunctionalService(IConfigService ConfigService)
    {
        var MapData = ConfigService.MapData;
        gridSystem = new GridSystem(MapData.width, MapData.height, MapData.cellSize);
        cellOutlinePrefab = ConfigService.MapData.cellOutlinePrefab;
        coordinatesPrefab = ConfigService.MapData.coordinatesPrefab;
    }

    public void InitialzieGrid()
    {
        Debug.Log("Initializing Grid");
        GridObject[,] gridObjectArray = gridSystem.getGridObjectArray();

        for (int x = 0; x < gridObjectArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjectArray.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjectArray[x,z].GetGridPosition();

                Transform cellOutlineTransform = GameObject.Instantiate(cellOutlinePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
            }
        }

        gridSystem.DisplayCoordinates(coordinatesPrefab);
    }
}