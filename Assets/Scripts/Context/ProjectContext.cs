using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContext
{
    private static ProjectContext instance;

    public static ProjectContext Instance
    {
        get{
            if(instance == null)
            instance = new ProjectContext();
            return instance;
        }
    }
    
    public IConfigService ConfigService {get; private set;}

    public IMapFunctionalService MapFunctionalService {get; private set;}
    public IMapGridTileService MapGridTileService {get; private set;}
    public IPathfindingService PathfindingService {get; private set;}

    public IUnitService UnitService {get; private set;}

    public IBattleService BattleService {get; private set;}

    public void Initialize(MapConfig mapConfig)
    {
        ConfigService = new ConfigService(mapConfig);

        MapFunctionalService = new MapFunctionalService(ConfigService);
        MapGridTileService = new MapGridTileService(ConfigService, MapFunctionalService);
        PathfindingService = new PathfindingService(ConfigService, MapFunctionalService, MapGridTileService);

        UnitService = new UnitService(MapFunctionalService);

        BattleService = new BattleService();
        Debug.Log("Loaded successfully");
    }
}
