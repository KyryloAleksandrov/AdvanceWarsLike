using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    [SerializeField] private LayerMask cellGridLayerMask;
    private GridSystem<GridObject> gridSystem;
    private GridOutline lastGridVisual;

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

        GridObject gridObject = gridSystem.GetGridObject(currentGridPosition);

        Debug.Log(gridObject.ToString());
    }
}
