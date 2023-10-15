using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using System;
using System.Security.Cryptography;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] List<GameObject> listTile;
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
        foreach(string line in filelines)
        {
            string[] sub = line.Split(' ');

            byte[] ascii = Encoding.ASCII.GetBytes(sub[0]);
            int type = int.Parse(ascii[0].ToString());
            type -= 65;

            int quantity = int.Parse(sub[1]);
            for (int i = 0; i < quantity; i++)
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
        foreach (int type in tileTypesList)
        {
            GameObject tile = Instantiate(listTile[type], spawnPos.transform.position, spawnPos.localRotation);
            tile.transform.parent = containPos;
            Rigidbody rigidbody = tile.GetComponent<Rigidbody>();
            rigidbody.AddRelativeForce(spawnPos.transform.forward * force);
            rigidbody.AddForce(Vector3.down * force);
            yield return new WaitForSeconds(0.4f);
        }
        isSpawning = false;
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


