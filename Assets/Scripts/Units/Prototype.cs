using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Prototype : Unit
{
    private List<Vector3> positionList;
    private int currentPositionIndex;

    private GridPosition currentGridPosition;
    private List<GridPosition> validGridPositions;
    private List<GridPosition> validFightPositions;

    private float moveSpeed = 5f;
    private float rotationSpeed = 15f;
    private float stoppingDistance = 0.1f;
    private float rotatingStoppingDistance = 0.5f;
    private bool isMoved;

    private int moveRadius;
    // Start is called before the first frame update
    void Start()
    {
        positionList = new List<Vector3>();
        validGridPositions = new List<GridPosition>();
        validFightPositions = new List<GridPosition>();
        currentGridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(transform.position);
        ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(currentGridPosition).SetUnit(this);
        //Debug.Log(currentGridPosition.ToString());

        moveRadius = 4;
        isMoved = false;

        ShowAvailablePositions();
        ShowAvailableFightPositions();
    }

    // Update is called once per frame
    void Update()
    {
        InitializeMove();
        InitializeFight();

        if(Input.GetKeyDown(KeyCode.H))
        {
            RefreshMove();
        }

        if(positionList.Count == 0)
        {
            return;
        }

        Vector3 targetPosition = positionList[currentPositionIndex];
        if(Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            
            /*Vector3 moveDirection = (targetPosition - transform.position).normalized;

            
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            
            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;*/

            RotateToMoveDirection();
            if(Vector3.Distance(transform.forward, GetMoveDirection()) < rotatingStoppingDistance)
            {
                GoForward();
            }
        }
        else{
            currentPositionIndex++;
            if(currentPositionIndex >= positionList.Count)
            {
                StopMoving();
            }
        }
        
    }

    private void InitializeMove()
    {
        if(Input.GetMouseButtonDown(0)) //TODO - refactor
        {
            Vector3 rawWorldPosition = MouseController.GetWorldPosition();
            GridPosition clickPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(rawWorldPosition);
            if(!ProjectContext.Instance.MapFunctionalService.gridSystem.IsInBounds(clickPosition))
            {
                return;
            }
            Vector3 toMove = ProjectContext.Instance.MapFunctionalService.gridSystem.GetWorldPosition(clickPosition);
            Move(toMove);
            //Debug.Log(currentPositionIndex);
            //Debug.Log(positionList.Count);
        }
    }

    private void InitializeFight()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector3 rawWorldPosition = MouseController.GetWorldPosition();
            GridPosition clickPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(rawWorldPosition);
            if(!validFightPositions.Contains(clickPosition))
            {
                return;
            }
            if(ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(clickPosition).GetUnit() != null)
            {
                Debug.Log("Fight Initiated");
            }
            else
            {
                Debug.Log("No enemy to fight");
            }
        }
    }

    private void Move(Vector3 targetPosition)
    {
        if(isMoved)
        {
            return;
        }

        GridPosition target = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(targetPosition);

        if(!GetValidMovePositions().Contains(target))
        {
            return;
        }

        List<GridPosition> pathGridPositionList = ProjectContext.Instance.PathfindingService.FindPath(currentGridPosition, target);

        if(pathGridPositionList == null)
        {
            Debug.Log("No Path");
            return;
        }

        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach(var pathPosition in pathGridPositionList)
        {
            positionList.Add(ProjectContext.Instance.MapFunctionalService.gridSystem.GetWorldPosition(pathPosition));
        }

        isMoved = true;
    }

    private Vector3 GetMoveDirection()
    {
        Vector3 targetPosition = positionList[currentPositionIndex];
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        return moveDirection; 
    }

    private void GoForward()
    {

        transform.position += GetMoveDirection() * moveSpeed * Time.deltaTime;
    }

    private void RotateToMoveDirection()
    {
        transform.forward = Vector3.Lerp(transform.forward, GetMoveDirection(), Time.deltaTime * rotationSpeed);
    }

    public void StopMoving()
    {
        currentGridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(positionList[positionList.Count - 1]);
        //Debug.Log(gridPosition.ToString());
        positionList.Clear();
        foreach(var position in validGridPositions)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridOutline().DeHilightToWalk();
        }
        foreach(var position in validFightPositions)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridOutline().DeHilightToFight();
        }
        validGridPositions.Clear();
        validFightPositions.Clear();
        if(!isMoved)
        {
            ShowAvailablePositions();
        }
        ShowAvailableFightPositions();
    }

    public List<GridPosition> GetValidMovePositions()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        
        for(int x = -moveRadius; x <= moveRadius; x++)
        {
            for(int z = -moveRadius; z <= moveRadius; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = currentGridPosition + offsetGridPosition;

                if(!ProjectContext.Instance.MapFunctionalService.gridSystem.IsInBounds(testGridPosition))
                {
                    continue;
                }
                
                if (currentGridPosition == testGridPosition)
                {
                    //position where unit is in already
                    continue;
                }

                if(ProjectContext.Instance.PathfindingService.GetPathLenght(currentGridPosition, testGridPosition) > moveRadius)
                {
                    continue;
                }


                validGridPositions.Add(testGridPosition);
            }
        }

        return validGridPositions;
    }

    public List<GridPosition> GetValidFightPositions()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        if(!isMoved)
        {
            List<GridPosition> movePositions = GetValidMovePositions();
            foreach(var position in movePositions)
            {
                List<GridPosition> neighbours = ProjectContext.Instance.MapFunctionalService.gridSystem.GetNeighbourPositions(position);
                foreach(var neighbourPosition in neighbours)
                {
                    if(neighbourPosition == currentGridPosition)
                    {
                        continue;
                    }
                    if(!movePositions.Contains(neighbourPosition) && !validGridPositions.Contains(neighbourPosition))
                    {
                        validGridPositions.Add(neighbourPosition);
                    }
                }
            }
        }
        else if(isMoved) 
        {
            validGridPositions = ProjectContext.Instance.MapFunctionalService.gridSystem.GetNeighbourPositions(currentGridPosition);
        }

        return validGridPositions;
    }

    public void ShowAvailablePositions()    
    {
        validGridPositions = GetValidMovePositions();
        foreach(var position in validGridPositions)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridOutline().HighlightToWalk();
            //Debug.Log(position.ToString());
        }
    }

    public void ShowAvailableFightPositions()
    {
        validFightPositions = GetValidFightPositions();
        foreach(var position in validFightPositions)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridOutline().HighlightToFight();
            //Debug.Log(position.ToString());
        }
    }
    public void RefreshMove()
    {
        foreach(var position in validFightPositions)
        {
            ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridObject(position).GetGridOutline().DeHilightToFight();
        }
        validFightPositions.Clear();
        isMoved = false;
        ShowAvailablePositions();
        ShowAvailableFightPositions();
    }

}
