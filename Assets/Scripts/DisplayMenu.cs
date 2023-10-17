using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMenu : MonoBehaviour
{
    [SerializeField] GameObject nextLevel;
    [SerializeField] GameObject retry;
    [SerializeField] GameObject returnMenu;
    Canvas canvas;
    bool isDisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetDisplayLose(!isDisplay);
        }
    }

    public void SetDisplayWin(bool value)
    {
        canvas.enabled = value;
        Win();
    }

    public void SetDisplayLose(bool value)
    {
        isDisplay = value;
        canvas.enabled = value;
        Lose();
    }

    void Win()
    {
        nextLevel.SetActive(true);
        retry.SetActive(true);
        returnMenu.SetActive(true);
    }

    void Lose()
    {
        nextLevel.SetActive(false);
        retry.SetActive(true);
        returnMenu.SetActive(true);
    }
}
