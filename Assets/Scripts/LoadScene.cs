using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] LoadLevel loadLevel;
    [SerializeField] int currentLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", currentLevel);
        }

        currentLevel = PlayerPrefs.GetInt("level");
        if (currentLevel > 3)
        {
            currentLevel = 1;
        }
        loadLevel.StartLevel(currentLevel);
    }

    public void NextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        SceneManager.LoadScene(1);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        PlayerPrefs.SetInt("level", currentLevel);
        SceneManager.LoadScene(1);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

}
