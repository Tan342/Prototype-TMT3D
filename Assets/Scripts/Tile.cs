using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite image;
    [SerializeField] public int tileType;
    public int id;

    public Sprite GetSprite()
    {
        return image;
    }

    public void SetSprite(Sprite sprite)
    {
        image = sprite;
    }
}
