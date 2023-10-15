using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Transform returnPos;

    List<Box> list = new List<Box>();
    List<Vector3> position = new List<Vector3>();

    int latestTile = -1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in transform)
        {
            list.Add(t.GetComponent<Box>());
            position.Add(t.transform.position);
        }
    }

    public void AddTile(Tile tile)
    {
        int amount = CountExist(tile);
        int index;
        if(amount > 0)
        {
            index = GetPosBoxExist(tile);
            if (HasOtherTileBehind(index))
            {
                MoveBoxBackwards(index + 1);
            }
            index++;
        }
        else
        {
            index = GetPosBox();
            
        }

        list[index].SetTile(tile);
        latestTile = index;

        if (CountExist(tile) >= 3)
        {
            scoreManager.AddScore(1);
            latestTile = -1;
            StartCoroutine(DeleteTile(tile,index));
        }
    }

    IEnumerator DeleteTile(Tile tile, int index)
    {
        yield return new WaitForSeconds(0.2f);
        foreach (Box box in list)
        {
            if (box.tileType == tile.tileType)
            {
                box.ResetTile();
            }
        }
        MoveBoxForwards(index);
    }

    void MoveBoxBackwards(int index)
    {
        list[list.Count - 1].SetPos(position[index]);
        for (int i = list.Count - 2; i >= index; i--)
        {
            list[i].Moving(position[i + 1]);
            Swap(i, i + 1);
        }
    }

    void MoveBoxForwards(int index)
    {
        if (HasOtherTileBehind(index))
        {
            for (int i = index + 1; i < list.Count; i++) 
            {
                list[i].Moving(position[i - 3]);
                list[i-3].SetPos(position[i]);
                Swap(i, i -3);
            }
        }
    }

    int GetPosBox()
    {
        int i = 0;
        foreach(Box box in list)
        {
            if (!box.IsUsed())
            {
                return i;
            }
            i++;
        }
        return -1;
    }

    int GetPosBoxExist(Tile tile)
    {
        for (int j = list.Count - 1; j >= 0; j--)
        {
            if (list[j].tileType == tile.tileType)
            {
                return j;
            }
        }
        return -1;
    }

    bool HasOtherTileBehind(int index)
    {
        for(int i =  index+1; i < list.Count; i++)
        {
            if (list[i].tileType != TileType.Null)
            {
                return true;
            }
        }
        return false;
    }

    int CountExist(Tile tile)
    {
        int count = 0;
        foreach(Box box in list)
        {
            if(box.tileType == tile.tileType)
            {
                count++;
            }
        }
        return count;
    }

    void Swap(int i, int j)
    {
        Box temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public void ReturnPreviousStep()
    {
        if(latestTile == -1)
        {
            return;
        }

        Instantiate(list[latestTile], returnPos);
        list[latestTile].ResetTile();
        if (HasOtherTileBehind(latestTile))
        {
            for (int i = latestTile + 1; i < list.Count; i++)
            {
                list[i].Moving(position[i - 1]);
                list[i - 1].SetPos(position[i]);
                Swap(i, i - 1);
            }
        }
        latestTile = -1;
    }
}
