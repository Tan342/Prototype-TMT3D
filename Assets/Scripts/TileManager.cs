using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] ListManager listManager;
    [SerializeField] DisplayMenu menu;

    Dictionary<int, Tile> listTile= new Dictionary<int, Tile>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shuffle()
    {
        foreach (KeyValuePair<int, Tile> entry in listTile)
        {
            Rigidbody rigidbody = entry.Value.GetComponent<Rigidbody>();
            int x = Random.Range(-600, 600);
            int y = Random.Range(-600, 600);
            int z = Random.Range(-600, 600);
            rigidbody.AddForce(new Vector3(x,y,z));

        }
    }

    public void HintSkill()
    {
        Tile tile = listManager.GetTileWithHighestNumber();
        if(tile == null)
        {
            tile = listTile.First().Value;
        }
        StartCoroutine(Hint(tile));
    }

    IEnumerator Hint(Tile tile)
    {
        List<Tile> listTileTemp = listTile.Values.ToList();
        int currentQuantity = listManager.CountExist(tile);
        int count = 0;

        for (int i = 0; i < listTileTemp.Count; i++)
        {
            if (listTileTemp[i].tileType == tile.tileType)
            {
                int id = listTileTemp[i].id;
                listManager.AddTile(listTile[id]);
                count++;

                if (count == 3 - currentQuantity)
                {
                    break;
                }
                yield return new WaitForSeconds(0.4f);
            }
            
            
        }
    }

    public void Win()
    {
        menu.WinDelay();
    }

    public int GetQuantity()
    {
        return listTile.Count;
    }

    public void ReturnPreviousStep()
    {
        listManager.ReturnPreviousStep();
    }

    public void AddTile(int id,Tile tile)
    {
        listTile.Add(id,tile);
    }

    public void DeleteTile(int id)
    {
        listTile.Remove(id);
    }
}
