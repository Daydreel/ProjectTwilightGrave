using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferBehavior : MonoBehaviour
{
    DayInputBuffer buffer;

    // Start is called before the first frame update
    void Start()
    {
        buffer = new DayInputBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        buffer.Listen();
    }
}
