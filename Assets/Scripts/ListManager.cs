using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    [SerializeField] TileManager tileManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] DisplayMenu menu;

    Tile[] list = new Tile[7];
    List<Vector3> position = new List<Vector3>();

    int latestTileIndex = -1;
    Vector3 latestTilePos;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            position.Add(t.transform.position);
        }
    }

    public void AddTile(Tile tile)
    {
        int amount = CountExist(tile);
        int index;
        if (amount > 0)
        {
            index = GetPosTileExist(tile);
            if (HasOtherTileBehind(index))
            {
                MoveTileBackwards(index + 1);
            }
            index++;
        }
        else
        {
            index = GetPosTile();

        }

        list[index] = tile;
        latestTileIndex = index;
        latestTilePos = tile.transform.position;
        tile.Moving(position[index], false);
        tileManager.DeleteTile(tile.id);
        

        if (CountExist(tile) == 3)
        {
            scoreManager.AddScore(1);
            latestTileIndex = -1;
            StartCoroutine(DeleteTile(tile, index));
        }
        else if (CountTileInList() == list.Length)
        {
            menu.LoseDelay();
            return;
        }

        if (tileManager.GetQuantity() == 0)
        {
            tileManager.Win();
        }
    }

    

    IEnumerator DeleteTile(Tile tile, int index)
    {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] != null && list[i].tileType == tile.tileType)
            {
                Destroy(list[i].gameObject);
                list[i] = null;
            }
        }

        MoveTileForwards(index, 3);
    }

    void MoveTileBackwards(int index)
    {
        for (int i = list.Length - 2; i >= index; i--)
        {
            if (list[i] != null)
            {
                list[i].Moving(position[i + 1], false);
                Swap(i, i + 1);
            }
        }
    }

    void MoveTileForwards(int index, int amount)
    {
        if (HasOtherTileBehind(index))
        {
            for (int i = index + 1; i < list.Length; i++)
            {
                if (list[i] != null)
                {
                    list[i].Moving(position[i - amount], false);
                    Swap(i, i - amount);
                }
            }
        }
    }

    int GetPosTile()
    {
        int i = 0;
        foreach (Tile tile in list)
        {
            if (tile == null)
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    int GetPosTileExist(Tile tile)
    {
        for (int j = list.Length - 1; j >= 0; j--)
        {
            if (list[j] != null && list[j].tileType == tile.tileType)
            {
                return j;
            }
        }
        return -1;
    }

    bool HasOtherTileBehind(int index)
    {
        for (int i = index + 1; i < list.Length; i++)
        {
            if (list[i] != null)
            {
                return true;
            }
        }
        return false;
    }

    public int CountExist(Tile tile)
    {
        int count = 0;
        foreach (Tile t in list)
        {
            if (t != null && t.tileType == tile.tileType)
            {
                count++;
            }
        }
        return count;
    }

    int CountTileInList()
    {
        int count = 0;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    void Swap(int i, int j)
    {
        Tile temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public void ReturnPreviousStep()
    {
        if (latestTileIndex == -1)
        {
            return;
        }

        list[latestTileIndex].Moving(latestTilePos, true);
        tileManager.AddTile(list[latestTileIndex].id, list[latestTileIndex]);
        list[latestTileIndex] = null;

        MoveTileForwards(latestTileIndex, 1);

        latestTileIndex = -1;
    }

    public Tile GetTileWithHighestNumber()
    {
        Tile temp = null;
        int max = 0;
        for(int i = 0; i < list.Length; i++)
        {
            if (list[i] != null)
            {
                int count = CountExist(list[i]);
                if (count > max)
                {
                    temp = list[i];
                    max = count;
                }
            }
        }
        return temp;
    }
}
