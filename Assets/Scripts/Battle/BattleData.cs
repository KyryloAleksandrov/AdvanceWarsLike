using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BattleData
{
    GridTileVisual attackerVisual;
    GridTileVisual defenderVisual;

    public BattleData(GridTileVisual attackerVisual, GridTileVisual defenderVisual)
    {
        this.attackerVisual = attackerVisual;
        this.defenderVisual = defenderVisual;
    }
}
