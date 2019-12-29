using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tournament {

    public Population Population;
    public CompetitorState[] States;

    public System.TimeSpan Time => System.DateTime.Now - m_StartTime;
    public System.TimeSpan MaxTime;

    public int Generation;

    public float MaxDistance;

    System.DateTime m_StartTime;

    public bool IsRunning => States != null && States.Any(s=>s.IsRunning) && Time < MaxTime;

    public Tournament(int maxCompetitors, float maxDistance, float maxSeconds)
    {
        Generation = 0;
        Population = new Population(maxCompetitors);
        MaxTime = System.TimeSpan.FromSeconds(maxSeconds);
        MaxDistance = maxDistance;
        States = new CompetitorState[maxCompetitors];
        for(int i=0;i<maxCompetitors;i++)
        {
            States[i] = new CompetitorState();
        }
    }

    public void StartRound()
    {
        if(IsRunning)
        {
            for(int i=0;i<States.Length;i++)
            {
                var individual = Population.Individuals[i];
                individual.Score = States[i].HadError ? 0 : States[i].Distance;
                Population.Individuals[i] = individual;
            }
            Evolution.DoSelection(Population);
        }
        Generation++;
        m_StartTime = System.DateTime.Now;
    }
}
