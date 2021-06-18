using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WThread
{
    public class WaitForSecondsRealtime
    {
        public float waitTime { get; set; }
        float m_WaitUntilTime = -1;

        public bool KeepWaiting()
        {
     
                if (m_WaitUntilTime < 0)
                {
                    m_WaitUntilTime = Time.realtimeSinceStartup + waitTime;
                  //  Debug.Log("?? start" + m_WaitUntilTime);
                }

                bool wait = Time.realtimeSinceStartup < m_WaitUntilTime;
               // Debug.Log(Time.realtimeSinceStartup+"       "+m_WaitUntilTime);
                if (!wait)
                {
                    // Reset so it can be reused.
                    Reset();
                }
                return !wait;
    
        }

        public WaitForSecondsRealtime(float time)
        {
            waitTime = time;
        }

        public void Reset()
        {
            m_WaitUntilTime = -1;
        }
    }
}

