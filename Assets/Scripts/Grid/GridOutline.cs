using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOutline : MonoBehaviour
{
    [SerializeField] MeshRenderer highlighted;
    private GridObject gridObject; 
    // Start is called before the first frame update
    void Start()
    {
        DeHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }

    public void Highlight()
    {
        highlighted.enabled = true;
    }
    public void DeHighlight()
    {
        highlighted.enabled = false;
    }
}
