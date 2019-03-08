﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decision : ScriptableObject {
    public virtual void Initialise(DayFSM FSM)
    {

    }

    public abstract bool Decide(DayFSM FSM);

    public virtual void Terminate(DayFSM FSM)
    {

    }
}
