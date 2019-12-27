using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population {

    public struct Individual
    {
        public Genome Genome;
        public float Score;
    }
    public List<Individual> Individuals = new List<Individual>();

    public Population(int size)
    {
        for(int i=0;i<size;i++)
        {
            var individual = new Individual()
            {
                Genome = new Genome(Random.Range(0, 99999))
            };
            Individuals.Add(individual);
        }
    }

}
