using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Translator {

    const float k_MinWheelSize = .2f;
    const float k_MaxWheelSize = .8f;

    const float k_MinExtensionDistance = 1;
    const float k_MaxExtensionDistance = 2;

    Genome m_Genome;

    public Vector2[] RootPoints;

    public Genome.Extension[] Extensions;

    public Color32 BodyColor => m_Genome.BodyColor;

    public int Wheel1Anchor => m_Genome.Wheel1Anchor;
    public float Wheel1Radius => Lerp(k_MinWheelSize, k_MaxWheelSize, m_Genome.Wheel1Radius);
    public Color32 Wheel1Color => m_Genome.Wheel1Color;

    public int Wheel2Anchor => m_Genome.Wheel2Anchor;
    public float Wheel2Radius => Lerp(k_MinWheelSize, k_MaxWheelSize, m_Genome.Wheel2Radius);
    public Color32 Wheel2Color => m_Genome.Wheel2Color;

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

        Extensions = new Genome.Extension[1];
        for(int i=0;i<Extensions.Length;i++)
        {
            Extensions[i] = genome.Extensions;
            Extensions[i].distanceB = Lerp(k_MinExtensionDistance, k_MaxExtensionDistance, Extensions[i].distanceB);
        }
    }
}
