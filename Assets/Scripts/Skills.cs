using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] ListManager listManager;
    List<Tile> listTile  = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnPreviousStep()
    {
        listManager.ReturnPreviousStep();
    }
}
