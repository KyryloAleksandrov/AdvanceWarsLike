using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUnitService
{
    void SpawnUnit(GridPosition gridPosition, Transform unitTransform);
}
public class UnitService : IUnitService
{
    private  GridSystem<GridObject> gridSystem;
    public UnitService(IMapFunctionalService mapFunctionalService)
    {
        gridSystem = mapFunctionalService.gridSystem;
    }
    public void SpawnUnit(GridPosition gridPosition, Transform unitTransform)
    {
        Transform newUnit = GameObject.Instantiate(unitTransform, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);
    }
}
