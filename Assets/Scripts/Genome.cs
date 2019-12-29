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

    public struct Wheel
    {
        public int Anchor;
        public float Radius;
        public float MaxSpeed;
        public float MaxTorque;
        public Color32 Color;
    }

    public float RootPosition1;
    public float RootPosition2;
    public float RootPosition3;

    public Extension Extensions;

    public Color32 BodyColor;

    public Wheel Wheel1;
    public Wheel Wheel2;

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

        Wheel1 = new Wheel()
        {
            Anchor = Random.Range(0, 99999),
            Radius = Random.value,
            MaxSpeed = Random.value,
            MaxTorque = Random.value
        };

        Wheel2 = new Wheel()
        {
            Anchor = Random.Range(0, 99999),
            Radius = Random.value,
            Color = RandomColor(),
            MaxSpeed = Random.value,
            MaxTorque = Random.value
        };
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
