using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMenu : MonoBehaviour
{
    [SerializeField] GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("level"))
        {
            continueButton.SetActive(false);
        }
        else
        {
            int level = PlayerPrefs.GetInt("level");
            if (level > 3) 
            {
                continueButton.SetActive(false);
            }
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
