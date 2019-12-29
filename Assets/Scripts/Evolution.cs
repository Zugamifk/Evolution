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
                var mi = Random.Range(0, keepCount);
                var di = mi;
                while(mi==di)
                {
                    di = Random.Range(0, keepCount);
                }
                var mom = population.Individuals[mi].Genome;
                var dad = population.Individuals[di].Genome;
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
        object child = new Genome();
        var crossoverPoint = Random.Range(1, Genome.Fields.Length - 1);
        Genome first, second;
        if (Random.value > .5f)
        {
            first = mom;
            second = dad;
        }
        else
        {
            first = dad;
            second = mom;
        }

        for (int i = 0; i < Genome.Fields.Length; i++)
        {
            var selection = i < crossoverPoint ? first : second;
            var value = Genome.Fields[i].GetValue(selection);
            Genome.Fields[i].SetValue(child, value);
        }

        return (Genome)child;
    }

    static Genome Mutate(Genome child)
    {
        foreach (var f in Genome.Fields)
        {
            object value = f.GetValue(child);
            value = MutateValue(value);
            f.SetValue(child, value);
        }

        return child;
    }

    static object MutateValue(object value)
    {
        if(value is System.Array)
        {
            return MutateArray((System.Array)value, value.GetType().GetElementType());
        }else if (value is float)
        {
            return MutateFloat((float)value);
        } else if(value is int)
        {
            return MutateInt((uint)value);
        }
        else if (value is Color32)
        {
            return MutateColor32((Color32)value);
        } else if (value is Genome.Extension)
        {
            return MutateExtension((Genome.Extension)value);
        } else if(value is Genome.Wheel)
        {
            return Mutatewheel((Genome.Wheel)value);
        }

        Debug.Log("Value can't mutate! Type is " + value.GetType());
        return value;
    }

    static System.Array MutateArray(System.Array value, System.Type elementType)
    {
        var l = Clamp(value.Length + (Random.value > .5f ? 0 : Random.value > .5f ? 1 : -1), Genome.MinExtensions, Genome.MaxExtensions);
        var a = new object[l];
        for (int i = 0; i < l; i++)
        {
            if (i < value.Length)
            {
                a[i] = MutateValue(value.GetValue(i));
            } else
            {
                a[i] = Genome.GetRandom(elementType);
            }
        }
        return value;
    }

    static float MutateFloat(float value)
    {
        var mutation = Pow(Random.value, 3) * k_MutationRate;
        return Clamp01(Random.Range(-mutation, mutation) + value);
    }

    static Color32 MutateColor32(Color32 value)
    {
        return new Color32(MutateByte(value.r), MutateByte(value.g), MutateByte(value.b), value.a);
    }

    static byte MutateByte(byte value)
    {
        return (byte)(value ^ (1 << Random.Range(0, 8)));
    }

    static uint MutateInt(uint value)
    {
        return value ^ (1u << Random.Range(0, 32));
    }

    static Genome.Extension MutateExtension(Genome.Extension value)
    {
        return new Genome.Extension()
        {
            edgePoint = MutateInt(value.edgePoint),
            distanceA = MutateFloat(value.distanceA),
            distanceB = MutateFloat(value.distanceB)
        };
    }

    static Genome.Wheel Mutatewheel(Genome.Wheel value)
    {
        return new Genome.Wheel()
        {
            Anchor = MutateInt(value.Anchor),
            Radius = MutateFloat(value.Radius),
            MaxTorque = MutateFloat(value.MaxTorque),
            MaxSpeed = MutateFloat(value.MaxSpeed),
            Color = MutateColor32(value.Color)
        };
    }
}
