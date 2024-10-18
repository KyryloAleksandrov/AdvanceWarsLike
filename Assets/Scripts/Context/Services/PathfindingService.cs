using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingService
{
    GridSystem<PathNode> gridSystem {get;}
    Transform pathNodePrefab {get;}

    void InitializePathfinding();
}
public class PathfindingService : IPathfindingService
{
    public GridSystem<PathNode> gridSystem {get;}
    public Transform pathNodePrefab {get;}

    public PathfindingService(IConfigService configService, IMapFunctionalService mapFunctionalService)
    {
        var MapData = configService.MapData;    //TODO - create a separate config for pathfinding
        gridSystem = mapFunctionalService.CreateGridSystem<PathNode>((GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(g, gridPosition));
        pathNodePrefab = MapData.pathNodePrefab;
    }

    public void InitializePathfinding()
    {
        


        gridSystem.DisplayCoordinates(pathNodePrefab);
    }
}
