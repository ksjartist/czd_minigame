using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject gameOverText;
    public TextMeshProUGUI timerText;
    public float time;
    public float msec;
    public float sec;
    //public float min;

    private void Start()
    {
        time = 25;
        StartCoroutine("StopWatch");
    }

    IEnumerator StopWatch()
    {
        while (true)
        {
            time -= Time.deltaTime;
            msec = (int)((time - (int)time) * 100);
            sec = (int)(time % 60);
            //min = (int)(time / 60 % 60);

            //timerText.text = string.Format("{0:00} : {1:00} : {2:00}", min, sec, msec);
            timerText.text = string.Format("{0:00} : {1:00}", sec, msec);

            if (time <= 0)
            {
                StopCoroutine("StopWatch");
                timerText.gameObject.SetActive(false);

            
                gameOverText.SetActive(true);
                Time.timeScale = 0;
            }
            yield return null;
        }
    }
}
