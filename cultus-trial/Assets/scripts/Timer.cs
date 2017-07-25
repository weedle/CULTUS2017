using UnityEngine;
using System.Collections.Generic;

public class Timer : MonoBehaviour {
    private int numTimers = 0;
    private float currTime = 0;
    Dictionary<int, float> timers = null;

	// USAGE: creates a new thing to store timer records
    private void init()
    {
        timers = new Dictionary<int, float>();
    }

	
	// USAGE: udpates the current time value according to real time
	void Update () {
	    if(numTimers > 0)
        {
            currTime += Time.deltaTime;
        }
	}


	// USAGE: adds a new timer corresponding to the given ID
	// NOTE: currently ID will also link to an AI-controlled Unit
    public void addTimer(int timerId)
    {
        if(timers == null)
            init();
		
        timers[timerId] = currTime;
        numTimers++;
    }



    public bool checkTimer(int timerId, float duration)
    {
        if (timers != null)
        {
            if (timers.ContainsKey(timerId))
            {
                if ((currTime - timers[timerId]) >= duration)
                {
                    addTimer(timerId);
                    return true;
                }
            }
        }
        else
        {
            init();
        }
        return false;
    }


	// USAGE: removes the timer corresponding to the given ID
    public void removeTimer(int timerId)
    {
        timers.Remove(timerId);
    }
}
