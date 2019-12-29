using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Translator {

    const float k_MinWheelSize = .2f;
    const float k_MaxWheelSize = 1.5f;

    const float k_MinTorque = 200;
    const float k_MaxTorque = 500;

    const float k_MinSpeed = 500;
    const float k_MaxSpeed = 1000;

    const float k_MinExtensionDistance = 1;
    const float k_MaxExtensionDistance = 2;

    Genome m_Genome;

    public Vector2[] RootPoints;

    public Genome.Extension[] Extensions;

    public Color32 BodyColor => m_Genome.BodyColor;

    public Genome.Wheel[] Wheels;

    public Translator(Genome genome)
    {
        m_Genome = genome;
        var c = 2*PI;
        RootPoints = new[]
        {
            new Vector2(2*Sin(c*genome.RootPosition1/3f), 2*Cos(c*genome.RootPosition1/3f)),
            new Vector2(2*Sin(c*(1f/3f+genome.RootPosition2/3f)), 2*Cos(c*(1f/3f+genome.RootPosition2/3f))),
            new Vector2(2*Sin(c*(2f/3f+genome.RootPosition3/3f)), 2*Cos(c*(2f/3f+genome.RootPosition3/3f)))
        };

        Extensions = new Genome.Extension[genome.Extensions.Length];
        for(int i=0;i<Extensions.Length;i++)
        {
            Extensions[i] = genome.Extensions[i];
            Extensions[i].distanceB = Lerp(k_MinExtensionDistance, k_MaxExtensionDistance, Extensions[i].distanceB);
        }

        Wheels = new Genome.Wheel[genome.Wheels.Length];
        for(int i=0;i<Wheels.Length;i++)
        {
            var w = genome.Wheels[i];
            Wheels[i] = new Genome.Wheel()
            {
                Anchor = w.Anchor,
                Radius = Lerp(k_MinWheelSize, k_MaxWheelSize, w.Radius),
                MaxTorque = Lerp(k_MinTorque, k_MaxTorque, w.MaxTorque),
                MaxSpeed = Lerp(k_MinSpeed, k_MaxSpeed, w.MaxSpeed),
                Color = w.Color
            };
        }
    }
}
