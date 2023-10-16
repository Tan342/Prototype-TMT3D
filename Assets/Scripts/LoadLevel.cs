using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;
using System.Security.Cryptography;
using System.Threading;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] Config config;
    [SerializeField] MouseEvent mouseEvent;
    [SerializeField] Timer timer;

    [SerializeField] TileManager tileManager;
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform spawnPos;
    [SerializeField] Transform containPos;
    [SerializeField] float force = 2f;
    [SerializeField] float rotateSpeed = 2f;

    bool isSpawning = false;
    List<int> tileTypesList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.dataPath + "/LevelDesign" + "/level" + "1" +".txt";
        LoadLevelFromFile(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            spawnPos.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            
        }
    }

    public void StartLevel(int level)
    {
        string filePath = Application.dataPath + "/LevelDesign" + "/level" + level + ".txt";
        LoadLevelFromFile(filePath);
    }

    void LoadLevelFromFile(string filePath)
    {
        List<string> filelines = File.ReadAllLines(filePath).ToList();

        float time = float.Parse(filelines[0]);
        timer.SetTime(time);

        for(int i = 1; i < filelines.Count; i++)
        {
            string[] sub = filelines[i].Split(' ');

            int type = int.Parse(sub[0]);
            int quantity = int.Parse(sub[1]);

            for (int j = 0; j < quantity; j++)
            {
                tileTypesList.Add(type);
            }
        }
        ShuffleList();
        StartCoroutine(Spawn());
        
    }

    IEnumerator Spawn()
    {
        isSpawning = true;
        int count = 0;
        foreach (int type in tileTypesList)
        {
            GameObject tile = Instantiate(tilePrefab, spawnPos.transform.position, spawnPos.localRotation);
            tile.transform.parent = containPos;

            SetupTile(tile, type, count);
            count++;

            yield return new WaitForSeconds(0.4f);
        }
        mouseEvent.StarGame();
        timer.StartCounting();
        isSpawning = false;
    }

    void SetupTile(GameObject tile, int type, int id)
    {
        Tile t = tile.GetComponent<Tile>();
        t.SetSprite(config.sprites[type]);
        t.tileType = type;
        t.id = id;
        tileManager.AddTile(id, t);

        Rigidbody rigidbody = tile.GetComponent<Rigidbody>();
        rigidbody.AddRelativeForce(spawnPos.transform.forward * (force - id * 10));
        rigidbody.AddForce(Vector3.down * force);
    }

    private static System.Random rng = new System.Random();
    void ShuffleList()
    {
        int n = tileTypesList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = tileTypesList[k];
            tileTypesList[k] = tileTypesList[n];
            tileTypesList[n] = value;
        }
    }

}


