using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Prototype : MonoBehaviour
{
    private List<Vector3> positionList;
    private int currentPositionIndex;

    private GridPosition gridPosition;

    private float moveSpeed = 5f;
    private float rotationSpeed = 15f;
    private float stoppingDistance = 0.1f;
    private float rotatingStoppingDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        positionList = new List<Vector3>();
        gridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(transform.position);
        Debug.Log(gridPosition.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        InitializeMove();

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
            Debug.Log(currentPositionIndex);
            Debug.Log(positionList.Count);
        }
    }

    private void Move(Vector3 targetPosition)
    {
        GridPosition target = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(targetPosition);
        List<GridPosition> pathGridPositionList = ProjectContext.Instance.PathfindingService.FindPath(gridPosition, target);
        currentPositionIndex = 0;
        positionList = new List<Vector3>();

        foreach(var pathPosition in pathGridPositionList)
        {
            positionList.Add(ProjectContext.Instance.MapFunctionalService.gridSystem.GetWorldPosition(pathPosition));
        }
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

    private void StopMoving()
    {
        gridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(positionList[positionList.Count - 1]);
        Debug.Log(gridPosition.ToString());
        positionList.Clear();
    }
}
