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
    // the timer is an index in the array with the value stored
    // being the most recent time this timer was accessed
    public void addTimer(int timerId)
    {
        if(timers == null)
            init();
		
        timers[timerId] = currTime;
        numTimers++;
    }

    // checkTimer will return true if duration microseconds
    // have passed since the timer was last accessed
    // it will also update the last access time for that timer
    // otherwise, it will return false

    // if checkTimer is called with an unused timerId, it will
    // add the timer and return TRUE (vacuous case)
    public bool checkTimer(int timerId, float duration)
    {
        if (timers != null)
        {
            if (timers.ContainsKey(timerId))
            {
                if ((currTime - timers[timerId]) >= duration)
                {
                    timers[timerId] = currTime;
                    return true;
                }
                return false;
            }
            else
            {
                addTimer(timerId);
                return true;
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
        numTimers--;
    }
}
