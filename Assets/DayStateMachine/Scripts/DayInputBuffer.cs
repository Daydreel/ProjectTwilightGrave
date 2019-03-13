using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DayInputBuffer
{
    //Every Input of the player adds a new input in the buffer as the first item. 
    //Dequeuing it will take the last input.

    LocalTimer timer;
    public Queue<PlayerInput> buffer;

    public float bufferTime = 0.8f;

    public DayInputBuffer()
    {
        Initialise();
    }

    public void Initialise()
    {
        timer = new LocalTimer();
        
        buffer = new Queue<PlayerInput>();
    }

    //Listen to the player input
    public void Listen()
    {

        if (Input.GetButtonDown(PlayerInput.Attack.ToString()))
        {
            buffer.Enqueue(PlayerInput.Attack);
        }

        if (Input.GetButtonDown(PlayerInput.Execution.ToString()))
        {
            buffer.Enqueue(PlayerInput.Execution);
        }

        if (Input.GetButtonDown(PlayerInput.Jump.ToString()))
        {
            buffer.Enqueue(PlayerInput.Jump);
        }

        if (Input.GetButtonDown(PlayerInput.Dodge.ToString()))
        {
            buffer.Enqueue(PlayerInput.Dodge);
        }



        bufferManager(bufferTime);

        //Debug.Log("buffer count : " + buffer.Count);
    }

    void bufferManager(float bufferTime = 0.3f)
    {
        //Call the timer for every tick
        timer.Update();

        if (buffer.Count >= 1 && !timer.isTimerOn)
        {
            timer.StartTimer();
        }

        if (buffer.Count <= 0 && timer.isTimerOn)
        {
            timer.StopTimer();
        }

        if (timer.time >= bufferTime)
        {
            buffer.Dequeue();
            timer.ResetTimer();
        }
    }
}
