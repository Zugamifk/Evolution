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

    public Genome.Wheel Wheel1;
    public Genome.Wheel Wheel2;

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

        Wheel1 = new Genome.Wheel()
        {
            Anchor = genome.Wheel1.Anchor,
            Radius = Lerp(k_MinWheelSize, k_MaxWheelSize, genome.Wheel1.Radius),
            MaxTorque = Lerp(k_MinTorque, k_MaxTorque, genome.Wheel1.MaxTorque),
            MaxSpeed = Lerp(k_MinSpeed, k_MaxSpeed, genome.Wheel1.MaxSpeed),
            Color = genome.Wheel1.Color
        };

        Wheel2 = new Genome.Wheel()
        {
            Anchor = genome.Wheel2.Anchor,
            Radius = Lerp(k_MinWheelSize, k_MaxWheelSize, genome.Wheel2.Radius),
            MaxTorque = Lerp(k_MinTorque, k_MaxTorque, genome.Wheel2.MaxTorque),
            MaxSpeed = Lerp(k_MinSpeed, k_MaxSpeed, genome.Wheel2.MaxSpeed),
            Color = genome.Wheel2.Color
        };
    }
}
