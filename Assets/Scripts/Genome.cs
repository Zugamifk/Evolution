using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public struct Genome {

    public float Wheel1Radius;
    public Color32 Wheel1Color;
    public float Wheel2Radius;
    public Color32 Wheel2Color;

    public Genome(int seed)
    {
        Random.InitState(seed);

        Wheel1Radius = Random.value;
        Wheel1Color = new Color32((byte)Random.Range(0,256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
        Wheel2Radius = Random.value;
        Wheel2Color = new Color32((byte)Random.Range(0,256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
    }

    // enumerate fields
    public static FieldInfo[] Fields;
    static Genome()
    {
        Fields = typeof(Genome).GetFields(BindingFlags.Instance | BindingFlags.Public);
    }
}
