using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tournament {

    public Population Population;
    public Genome Current => Population.Individuals[m_CurrentIndex].Genome;

    public System.TimeSpan Time => System.DateTime.Now - m_StartTime;
    public System.TimeSpan MaxTime;

    public int Individual => m_CurrentIndex;
    public int Generation;

    public float Distance;
    public float MaxDistance;

    public bool IsRunning => m_CurrentIndex >= 0;
    public bool IsTestOver => Time > MaxTime || Distance > MaxDistance;
    public bool IsRoundOver => m_CurrentIndex >= Population.Individuals.Count;

    System.DateTime m_StartTime;
    int m_CurrentIndex = -1;

    public Tournament(int maxCompetitors, float maxDistance, float maxSeconds)
    {
        Generation = 0;
        Population = new Population(maxCompetitors);
        MaxTime = System.TimeSpan.FromSeconds(maxSeconds);
        MaxDistance = maxDistance;
    }

    public void StartRound()
    {
        if(IsRunning)
        {
            Evolution.DoSelection(Population);
        }
        Generation++;
        m_CurrentIndex = -1;
        StartTest();
    }

    public void StartTest()
    {
        if (IsRunning) AssignScore();

        m_CurrentIndex++;
        m_StartTime = System.DateTime.Now;
        Distance = 0;
    }

    void AssignScore()
    {
        var current = Population.Individuals[m_CurrentIndex];
        current.Score = Distance / MaxDistance;
        Population.Individuals[m_CurrentIndex] = current;
    }
}
