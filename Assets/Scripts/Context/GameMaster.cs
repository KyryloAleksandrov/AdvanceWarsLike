using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    private IMapFunctionalService mapFunctionalService;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mapFunctionalService = ProjectContext.Instance.MapFunctionalService;
    }
    // Start is called before the first frame update
    void Start()
    {
        mapFunctionalService.InitialzieGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
