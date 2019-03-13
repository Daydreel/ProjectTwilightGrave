using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTimer
{
    //Global as a gameManager or attached to it ;)

    public LocalTimer()
    {
        time = 0;
    }

    #region Timer's Variables

    [HideInInspector]
    public float time = 0;
    public bool isTimerOn = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {
        if (isTimerOn)
        {
            time += Time.deltaTime;
        }
    }

    #region Timer's Functions

    public void StartTimer()
    {
        ResetTimer();
        isTimerOn = true;
    }

    public void PauseTimer()
    {
        isTimerOn = false;
    }

    public void StopTimer()
    {
        PauseTimer();
        ResetTimer();
    }

    public void ResetTimer(float value = 0)
    {
        time = value;
    }

    public void CountDown(float value)
    {
        //TODO
    }

    #endregion
}
