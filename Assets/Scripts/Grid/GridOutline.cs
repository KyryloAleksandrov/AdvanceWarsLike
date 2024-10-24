using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOutline : MonoBehaviour
{
    [SerializeField] private MeshRenderer highlighted;
    [SerializeField] private MeshRenderer highlightedToWalk;
    [SerializeField] private MeshRenderer highlightedToFight;
    private GridObject gridObject; 
    // Start is called before the first frame update
    void Start()
    {
        DeHighlight();
        DeHilightToWalk();
        DeHilightToFight();
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

    public void HighlightToWalk()
    {
        highlightedToWalk.enabled = true;
    }
    public void DeHilightToWalk()
    {
        highlightedToWalk.enabled = false;
    }

    public void HighlightToFight()
    {
        highlightedToFight.enabled = true;
    }
    public void DeHilightToFight()
    {
        highlightedToFight.enabled = false;
    }

}
