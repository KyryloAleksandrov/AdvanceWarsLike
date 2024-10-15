using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapGridTileService mapGridTileService;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapFunctionalService = ProjectContext.Instance.MapFunctionalService;
        mapGridTileService = ProjectContext.Instance.MapGridTileService;
    }
    // Start is called before the first frame update
    void Start()
    {
        mapFunctionalService.InitializeGrid();
        mapGridTileService.InitializeTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
