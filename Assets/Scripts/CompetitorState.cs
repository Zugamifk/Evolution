using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorState {
    float m_StartPosition = 25;
    float m_LastPosition = 0;
    int m_RestFrames = 0;

    const float k_MaxStep = 2; // to detect physics glitches
    const float k_MinStep = 0.001f;
    const int k_MaxRestFrames = 15; // to detect when not moving
    const float k_MaxRetreat = -5; // to detect going backwards

    public bool HadError;
    public bool IsRunning;
    public float Distance => m_LastPosition - m_StartPosition;

    public void Initialize(float position)
    {
        m_StartPosition = position;
        m_LastPosition = position;
        m_RestFrames = 0;
        HadError = false;
        IsRunning = true;
    }

    public void Update(float position)
    {
        var step = Mathf.Abs(position - m_LastPosition);
        if (step > k_MaxStep)
        {
            IsRunning = false;
            HadError = true;
        }
        else if (m_LastPosition != m_StartPosition && step <= k_MinStep)
        {
            m_RestFrames++;
            if (m_RestFrames > k_MaxRestFrames)
            {
                // stopped
                IsRunning = false;
            }
        }
        else if (position - m_StartPosition < k_MaxRetreat)
        {
            //  going backwards
            IsRunning = false;
            HadError = true;
        }
        else
        {
            m_RestFrames = 0;
        }

        m_LastPosition = position;
    }
}
