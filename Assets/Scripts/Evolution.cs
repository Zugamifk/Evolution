using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public static class Evolution
{
    const float k_MutationRate = 0.1f;

    public static void DoSelection(Population population)
    {
        float score = 0;
        float max = float.MinValue, min = float.MaxValue;
        foreach (var i in population.Individuals)
        {
            max = Max(max, i.Score);
            min = Min(min, i.Score);
            score += i.Score;
        }
        Debug.Log($"Mean Score: {score / population.Individuals.Count}\tMin Score: {min}\tMax Score: {max}");

        population.Individuals.Sort((a, b) => (int)Sign(b.Score - a.Score));
        var newPopulation = new List<Genome>();
        var keepCount = population.Individuals.Count / 2;
        for (int i = 0; i < population.Individuals.Count; i++)
        {
            if (i < keepCount)
            {
                newPopulation.Add(population.Individuals[i].Genome);
            }
            else
            {
                var mom = population.Individuals[Random.Range(0, keepCount)].Genome;
                var dad = population.Individuals[Random.Range(0, keepCount)].Genome;
                var child = Crossover(mom, dad);
                child = Mutate(child);
                newPopulation.Add(child);
            }
        }

        population.Individuals.Clear();
        foreach (var i in newPopulation)
        {
            population.Individuals.Add(new Population.Individual()
            {
                Genome = i
            });
        }
    }

    static Genome Crossover(Genome mom, Genome dad)
    {
        var child = new Genome();
        var crossoverPoint = Random.Range(1, Genome.Fields.Length - 1);
        Genome first, second;
        if(Random.value > .5f)
        {
            first = mom;
            second = dad;
        } else
        {
            first = dad;
            second = mom;
        }

        for(int i=0;i<Genome.Fields.Length;i++)
        {
            var selection = i < crossoverPoint ? first : second;
            var value = Genome.Fields[i].GetValue(selection);
            Genome.Fields[i].SetValue(child, value);
        }

        return child;
    }

    static Genome Mutate(Genome child)
    {
        foreach(var f in Genome.Fields)
        {
            object value = f.GetValue(child);
            value = MutateValue(value);
            f.SetValue(child, value);
        }

        return child;
    }

    static object MutateValue(object value)
    {
        if(value is float)
        {
            var mutation = Pow(Random.value, 3) * k_MutationRate;
            return Clamp01(Random.Range(-mutation, mutation) + (float)value);
        }

        return null;
    }
}
