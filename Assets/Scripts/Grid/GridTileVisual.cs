using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] private int walkCost;
    [SerializeField] private MeshRenderer tile;

    // Start is called before the first frame update
    void Start()
    {
        walkCost = 0;
        ChangeMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetWalkCost()
    {
        return walkCost;
    }

    public void SetWalkCost(int walkCost)
    {
        this.walkCost = walkCost;
    }

    public void IncreaseCost()
    {
        if(walkCost == 3)
        {
            walkCost = 0;
        }
        else
        {
            walkCost = walkCost + 1; 
        }
        ChangeMaterial();
    }

    public void ChangeMaterial()
    {
        switch(walkCost)
        {
            case 0:
                tile.material.color = Color.gray;
                break;

            case 1:
                tile.material.color = Color.green;
                break;

            case 2:
                tile.material.color = Color.blue;
                break;
            
            case 3:
                tile.material.color = Color.red;
                break;
        }
    }
}
