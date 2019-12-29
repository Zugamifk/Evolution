using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public struct Genome {

    public struct Extension
    {
        public int edgePoint;
        public float distanceA, distanceB;
    }

    public float RootPosition1;
    public float RootPosition2;
    public float RootPosition3;

    public Extension Extensions;

    public Color32 BodyColor;

    public int Wheel1Anchor;
    public float Wheel1Radius;
    public Color32 Wheel1Color;
    public int Wheel2Anchor;
    public float Wheel2Radius;
    public Color32 Wheel2Color;

    public Genome(int seed)
    {
        Random.InitState(seed);

        RootPosition1 = Random.value;
        RootPosition2 = Random.value;
        RootPosition3 = Random.value;

        Extensions = new Extension()
        {
            edgePoint = Random.Range(0, 100000),
            distanceA = Random.value,
            distanceB = Random.value
        };

        BodyColor = RandomColor();

        Wheel1Anchor = Random.Range(0, 99999);
        Wheel1Radius = Random.value;
        Wheel1Color = RandomColor();
        Wheel2Anchor = Random.Range(0, 99999);
        Wheel2Radius = Random.value;
        Wheel2Color = RandomColor();
    }

    static Color32 RandomColor()
    {
        return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
    }

    // enumerate fields
    public static FieldInfo[] Fields;
    static Genome()
    {
        Fields = typeof(Genome).GetFields(BindingFlags.Instance | BindingFlags.Public);
    }
}
