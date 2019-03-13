using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    //Global as a gameManager or attached to it ;)

    protected TimerManager() { }

    #region Timer's Variables

    [HideInInspector]
    public float time = 0;
    bool isTimerOn = false;

    #endregion

    // Start is called before the first frame update
    public void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
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
