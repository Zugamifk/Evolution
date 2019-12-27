using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population {

    public List<Genome> Individuals = new List<Genome>();

    public Population(int size)
    {
        for(int i=0;i<size;i++)
        {
            Individuals.Add(new Genome(Random.Range(0, 99999)));
        }
    }

}
