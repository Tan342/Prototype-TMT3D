using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite image;
    [SerializeField] public TileType tileType;

    public Sprite GetSprite()
    {
        return image;
    }

}
