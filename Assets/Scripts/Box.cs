using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    Image image;
    public int tileType;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        tileType = -1;
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = pos;
    }

    public bool IsUsed()
    {
        if(image.sprite == null) return false;
        return true;
    }

    public void SetTile(Tile tile)
    {
        tileType = tile.tileType;
        image.sprite = tile.GetSprite();
    }

    public void ResetTile()
    {
        tileType = -1;
        image.sprite = null;
    }

    public void Moving(Vector3 end)
    {
        StartCoroutine(StartMoving(end));
    }

    IEnumerator StartMoving(Vector3 end)
    {
        Vector3 start = transform.position;
        float timer = 0;

        while (timer <= 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, timer * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
