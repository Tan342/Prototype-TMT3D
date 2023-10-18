using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseEvent : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [SerializeField] ListManager listManager;
    [SerializeField] AudioManager audioManager;
    bool isProcessing = false;
    bool isPlaying = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && isPlaying)
        {
            if(!isProcessing)
            {
                StartCoroutine(Processing());
                isProcessing = true;
            }
        }
    }

    public void StarGame()
    {
        isPlaying = true;
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
            audioManager.PlayPickUpSound();
        }
        yield return new WaitForSeconds(0.25f);
        isProcessing =  false;
    }
}
