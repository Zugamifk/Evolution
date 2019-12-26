using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evolution {

    List<Competitor> m_Population = new List<Competitor>();

    public void AddToPopulation(Competitor competitor)
    {
        m_Population.Add(competitor);
    }

    void DoSelection()
    {
        // select by fitness
    }

    Genome Crossover(Genome mom, Genome dad)
    {
        var child = new Genome();

        return child;
    }

    Genome Mutate(Genome child)
    {
        return child;
    }
}
