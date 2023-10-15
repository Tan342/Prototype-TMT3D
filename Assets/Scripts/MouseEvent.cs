using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] ListManager listManager;
    bool isProcessing = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!isProcessing)
            {
                StartCoroutine(Processing());
                isProcessing = true;
            }
        }
    }

    IEnumerator Processing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mask))
        {
            var selection = raycastHit.transform;
            var selectionTile = selection.GetComponent<Tile>();
            if (selectionTile != null)
            {
                listManager.AddTile(selectionTile);
            }
        }
        yield return new WaitForSeconds(0.5f);
        isProcessing =  false;
    }
}
