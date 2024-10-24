using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private Transform unitPrototype;
    [SerializeField] private Transform enemyPrototype;
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
        unitService.SpawnUnit(new GridPosition(3, 3), enemyPrototype);   
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

        if(Input.GetKeyDown(KeyCode.B))
        {
            LoadSceneSeparately("Battle");
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            UnloadAdditiveScene("Battle");
        }
    }

    public void LoadSceneSeparately(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += OnSceneLoaded;
    }

    public void OnSceneLoaded(AsyncOperation operation)
    {
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        Debug.Log(newScene.name);
        SceneManager.SetActiveScene(newScene);
    }

    public void UnloadAdditiveScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}
