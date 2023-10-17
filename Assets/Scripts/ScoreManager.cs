using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] float timeCombo = 5f;
    [SerializeField] TextMeshProUGUI scoresText;
    [SerializeField] Slider comboSlider;
    [SerializeField] TextMeshProUGUI comboText;

    int score;
    int combo = 1;
    bool isCombo = false;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        ComboDisplay(false);
    }

    private void Update()
    {
        if(isCombo)
        {
            timer += Time.deltaTime;
            comboSlider.value = 1 - timer / timeCombo;
            if(timer>timeCombo)
            {
                combo = 1;
                isCombo = false;
                ComboDisplay(false);
            }
        }

    }

    public void AddScore(int s)
    {
        score += s * combo;
        scoresText.text = "Score: " + score;
        combo++;
        comboText.text = "x" + combo;
        audioManager.PlayScoringSound();
        if (!isCombo)
        {
            isCombo = true;
            ComboDisplay(true);
        }

        timer = 0f;
    }

    void ComboDisplay(bool value)
    {
        comboText.enabled = value;
        comboSlider.enabled = value;
    }



}
