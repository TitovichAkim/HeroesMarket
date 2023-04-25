using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [Range(1, 40)]
    public int timeScaler;

    public void Start ()
    {
        timeScaler = 1;
    }
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScaler;
    }
}
