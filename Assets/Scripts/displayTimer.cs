using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class to show game running time on the screen.
public class displayTimer : MonoBehaviour
{
    //Shows time game has been running in GUI.
    public Text timerText;

    //Cleaned up text formatting and stored here
    private string niceTime;

    //Current time 
    private float timer = 0.0f;
    private int minutes = 0;
    private int seconds = 0;

    // Update is called once per frame
    void Update()
    {
        updateTime();
    }

    private void updateTime()
    {
        timer += Time.deltaTime;
    }

    private void OnGUI()
    {
        //Format time from float to standard time display (0:00). 
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);
        niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = string.Format("{0:HH:mm:ss}", niceTime);
    }
}
