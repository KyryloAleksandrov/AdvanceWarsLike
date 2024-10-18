using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Prototype : MonoBehaviour
{
    private Vector3 targetPosition;
    private float moveSpeed = 5f;
    private float rotationSpeed = 15f;
    private float stoppingDistance = 0.1f;
    private float rotatingStoppingDistance = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


        if(Input.GetMouseButtonDown(0))
        {
            Vector3 rawWorldPosition = MouseController.GetWorldPosition();
            GridPosition clickPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(rawWorldPosition);
            if(!ProjectContext.Instance.MapFunctionalService.gridSystem.IsInBounds(clickPosition))
            {
                return;
            }
            Vector3 toMove = ProjectContext.Instance.MapFunctionalService.gridSystem.GetWorldPosition(clickPosition);
            Move(toMove);
        }
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private Vector3 GetMoveDirection()
    {
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
}
