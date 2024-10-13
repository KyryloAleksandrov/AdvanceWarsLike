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

    public void Initialize(MapConfig mapConfig)
    {
        ConfigService = new ConfigService(mapConfig);

        MapFunctionalService = new MapFunctionalService(ConfigService);
        Debug.Log("Loaded successfully");
    }
}
