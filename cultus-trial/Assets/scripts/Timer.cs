using UnityEngine;
using System.Collections.Generic;

public class Timer : MonoBehaviour {
    private int numTimers = 0;
    private float currTime = 0;
    Dictionary<int, float> timers = null;
	// Use this for initialization
	void Start () {
	}

    private void init()
    {
        timers = new Dictionary<int, float>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(numTimers > 0)
        {
            currTime += Time.deltaTime;
        }
	}

    public void addTimer(int timerId)
    {
        if(timers == null)
        {
            init();
        }
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

    public void removeTimer(int timerId)
    {
        timers.Remove(timerId);
    }
}
