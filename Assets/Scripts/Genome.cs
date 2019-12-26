using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Genome {
    public float Wheel1Radius;
    public float Wheel2Radius;

    public Genome(int seed)
    {
        Random.InitState(seed);
        Wheel1Radius = Random.value;
        Wheel2Radius = Random.value;
    }
}
