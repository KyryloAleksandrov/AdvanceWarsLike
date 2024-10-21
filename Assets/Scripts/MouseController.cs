using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    private static MouseController Instance;
    [SerializeField] private LayerMask cellGridLayerMask;
    private GridSystem<GridObject> gridSystem;
    private GridOutline lastGridVisual;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridSystem = ProjectContext.Instance.MapFunctionalService.gridSystem;
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        HighlightOnHover(GetFirstLayerMask());
    }

    public static Vector3 GetWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, Instance.cellGridLayerMask);
        return raycastHit.point;
    }

    public LayerMask GetFirstLayerMask()
    {
        LayerMask hitLayerMask = default;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            int layer = hit.collider.gameObject.layer;
            hitLayerMask = 1 << layer;
        }

        return hitLayerMask;
    }

    public void HighlightOnHover(LayerMask layerMask)
    {
        if(layerMask != cellGridLayerMask)
        {
            if(lastGridVisual != null)
            {
                lastGridVisual.DeHighlight();
            }
        }
        else if(layerMask == cellGridLayerMask)
        {
            HilightCell();
            if(Input.GetMouseButtonDown(0))
            {
                ClickOnHex();
            }
            if(Input.GetMouseButtonDown(1))
            {
                IncreaseTileCost();
            }
        }
    }

    public void HilightCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, cellGridLayerMask);
        if(isOnGrid)
        {
            GridPosition currentGridPosition = gridSystem.GetGridPosition(raycastHit.point);
            if(gridSystem.IsInBounds(currentGridPosition))
            {
                if(lastGridVisual != null)
                {
                    lastGridVisual.DeHighlight();
                }

                lastGridVisual = gridSystem.GetGridObject(currentGridPosition).GetGridOutline();
                if(lastGridVisual != null)
                {
                    lastGridVisual.Highlight();
                }
            }
        }
        else
        {
            if(lastGridVisual != null)
            {
                lastGridVisual.DeHighlight();
            }
        }
    }

    public void ClickOnHex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, cellGridLayerMask);
        
        if(!isOnGrid)
        {
            return;
        }

        GridPosition currentGridPosition = gridSystem.GetGridPosition(raycastHit.point);

        /*GridObject gridObject = gridSystem.GetGridObject(currentGridPosition);

        Debug.Log(gridObject.ToString());*/

        GridTile gridTile = ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(currentGridPosition);
        //Debug.Log(gridTile.GetGridTileVisual());
    }

    public void IncreaseTileCost()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOnGrid = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, cellGridLayerMask);
        
        if(!isOnGrid)
        {
            return;
        }

        GridPosition currentGridPosition = gridSystem.GetGridPosition(raycastHit.point);

        GridTile gridTile = ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(currentGridPosition);
        gridTile.GetGridTileVisual().IncreaseCost();
    }
}
