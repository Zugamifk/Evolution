using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public struct Genome {

    public const int MinExtensions = 2;
    public const int MaxExtensions = 10;

    public struct Extension
    {
        public uint edgePoint;
        public float distanceA, distanceB;
    }

    public struct Wheel
    {
        public uint Anchor;
        public float Radius;
        public float MaxSpeed;
        public float MaxTorque;
        public Color32 Color;
    }

    public float RootPosition1;
    public float RootPosition2;
    public float RootPosition3;

    public Extension[] Extensions;

    public Color32 BodyColor;

    public Wheel Wheel1;
    public Wheel Wheel2;

    public Genome(int seed)
    {
        Random.InitState(seed);

        RootPosition1 = Random.value;
        RootPosition2 = Random.value;
        RootPosition3 = Random.value;

        int ec = Random.Range(MinExtensions, MaxExtensions);
        Extensions = new Extension[ec];
        for(int i=0;i<ec;i++)
        {
            Extensions[i] = RandomExtension();
        }

        BodyColor = RandomColor();

        Wheel1 = RandomWheel();
        Wheel2 = RandomWheel();
    }

    public static object GetRandom(System.Type type)
    {
        /*if (value is System.Array)
        {
            return MutateArray((System.Array)value);
        }
        else*/ 
        if (type == typeof(float))
        {
            return Random.value;
        }
        else if (type == typeof(int))
        {
            return Random.Range(0,100000);
        }
        else if (type == typeof(Color32))
        {
            return RandomColor();
        }
        else if (type == typeof(Extension))
        {
            return RandomExtension();
        }
        else if (type == typeof(Wheel))
        {
            return RandomWheel();
        }

        return null;
    }

    static Color32 RandomColor()
    {
        return new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
    }

    static Extension RandomExtension()
    {
        return new Extension()
        {
            edgePoint = (uint)Random.Range(0, 100000),
            distanceA = Random.value,
            distanceB = Random.value
        };
    }

    static Wheel RandomWheel()
    {
        return new Wheel()
        {
            Anchor = (uint)Random.Range(0, 99999),
            Radius = Random.value,
            Color = RandomColor(),
            MaxSpeed = Random.value,
            MaxTorque = Random.value
        };
    }

    // enumerate fields
    public static FieldInfo[] Fields;
    static Genome()
    {
        Fields = typeof(Genome).GetFields(BindingFlags.Instance | BindingFlags.Public);
    }
}
