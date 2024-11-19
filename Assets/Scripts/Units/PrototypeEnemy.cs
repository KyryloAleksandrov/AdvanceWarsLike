using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeEnemy : Unit
{
    private GridPosition currentGridPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentGridPosition = ProjectContext.Instance.MapFunctionalService.gridSystem.GetGridPosition(transform.position);
        ProjectContext.Instance.MapGridTileService.gridSystem.GetGridObject(currentGridPosition).SetUnit(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
