using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform unitPrototype;
    private Prototype prototype;

    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;
    private IMapGridTileService mapGridTileService;
    private IPathfindingService pathfindingService;

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
        pathfindingService = ProjectContext.Instance.PathfindingService;
        

        unitService = ProjectContext.Instance.UnitService;
    }
    // Start is called before the first frame update
    void Start()
    {
        mapFunctionalService.InitializeGrid();
        mapGridTileService.InitializeTiles();
        pathfindingService.InitializePathfinding();

        prototype = unitPrototype.GetComponent<Prototype>();
        unitService.SpawnUnit(new GridPosition(0, 0), unitPrototype);   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GridPosition mouseGridPosition = mapFunctionalService.gridSystem.GetGridPosition(MouseController.GetWorldPosition());
            GridPosition startGridPosition = new GridPosition(0, 0);

            List<GridPosition> gridPositionList = pathfindingService.FindPath(startGridPosition, mouseGridPosition);
            
            for(int i = 0; i < gridPositionList.Count - 1; i++)
            {
                Debug.DrawLine(mapFunctionalService.gridSystem.GetWorldPosition(gridPositionList[i]), mapFunctionalService.gridSystem.GetWorldPosition(gridPositionList[i + 1]), Color.red, 10f);
            }
            Debug.Log(pathfindingService.GetPathLenght(startGridPosition, mouseGridPosition));
        }

        
    }
}
