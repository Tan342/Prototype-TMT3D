using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupTile : MonoBehaviour
{
    Tile tile;
    SpriteRenderer spriteRenderer;
    MeshRenderer meshRenderer;
    Color originColor;

    // Start is called before the first frame update
    void Start()
    {
        tile = GetComponent<Tile>();    
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = tile.GetSprite();
        meshRenderer = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
    }

    private void Update()
    {
        /*if (transform.rotation.x < -160 || transform.rotation.x > 160
            || transform.rotation.z < -160 || transform.rotation.z > 160)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.up);
        }*/
    }

    private void OnMouseDown()
    {
        meshRenderer.material.color = Color.black;
    }

    private void OnMouseUp()
    {
        meshRenderer.material.color = originColor;
    }
}
