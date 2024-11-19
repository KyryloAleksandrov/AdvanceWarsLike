using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBattleService
{
    BattleData battleData {get; set;}
    void CreateBattleData(GridTileVisual attackerVisual, GridTileVisual defenderVisual);
    
}
public class BattleService : IBattleService
{
    public BattleData battleData {get; set;}
    public void CreateBattleData(GridTileVisual attackerVisual, GridTileVisual defenderVisual)
    {
        battleData = new BattleData(attackerVisual, defenderVisual);
    }
}
