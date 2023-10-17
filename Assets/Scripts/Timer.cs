using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] DisplayMenu menu;
    [SerializeField] TextMeshProUGUI textMeshPro;

    float time = 300;
    bool isCounting = false;
    float minutes;
    float seconds;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro.text = "0:00";
    }

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            CountingTime();
            if(time <= 0)
            {
                menu.SetDisplayLose(true);
            }
        }
    }

    public void FreezeTime()
    {
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        isCounting = false;
        yield return new WaitForSeconds(10);
        isCounting = true;
    }

    public void StartCounting()
    {
        isCounting = true;
    }

    public void StopCounting()
    {
        isCounting = false;
    }

    public void SetTime(float time)
    {
        this.time = time;
        Calculate();
        textMeshPro.text = minutes + ":" + seconds;
    }

    void CountingTime()
    {
        time -= Time.deltaTime;
        Calculate();
        textMeshPro.text = minutes + ":" + seconds;
    }

    void Calculate()
    {
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);
    }
}
