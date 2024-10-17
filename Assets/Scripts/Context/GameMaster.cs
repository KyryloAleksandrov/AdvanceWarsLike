using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform unitPrototype;

    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapGridTileService mapGridTileService;

    private IUnitService unitService;

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
        unitService = ProjectContext.Instance.UnitService;
    }
    // Start is called before the first frame update
    void Start()
    {
        mapFunctionalService.InitializeGrid();
        mapGridTileService.InitializeTiles();

        unitService.SpawnUnit(new GridPosition(0, 0), unitPrototype);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
