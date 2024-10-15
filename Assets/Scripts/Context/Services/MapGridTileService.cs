using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMapGridTileService
{
    GridSystem<GridTile> gridSystem {get;}
    Transform tilePrefab {get;}
    
    void InitializeTiles();
}

public class MapGridTileService : IMapGridTileService
{
    public GridSystem<GridTile> gridSystem {get;}
    public Transform tilePrefab {get;}

    public MapGridTileService(IConfigService configService, IMapFunctionalService mapFunctionalService)
    {
        var MapData = configService.MapData;    //TODO - create a separate config that specifies tiles
        tilePrefab = MapData.tilePrefab;

        //gridSystem = new GridSystem<GridTile>(MapData.width, MapData.height, MapData.cellSize,(GridSystem<GridTile> g, GridPosition gridPosition) => new GridTile(g, gridPosition));
        gridSystem = mapFunctionalService.CreateGridSystem<GridTile>((GridSystem<GridTile> g, GridPosition gridPosition) => new GridTile(g, gridPosition));
        
    }

    public void InitializeTiles()
    {
        GridTile[,] gridObjectArray = gridSystem.getGridObjectArray();

        for (int x = 0; x < gridObjectArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridObjectArray.GetLength(1); z++)
            {
                GridPosition gridPosition = gridObjectArray[x,z].GetGridPosition();

                Transform tileTransform = GameObject.Instantiate(tilePrefab, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
            }
        }
    }
}
