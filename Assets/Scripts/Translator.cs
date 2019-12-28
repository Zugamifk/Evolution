using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class Translator {

    const float k_MinWheelSize = .2f;
    const float k_MaxWheelSize = .8f;

    Genome m_Genome;

    public float Wheel1Radius => Lerp(k_MinWheelSize, k_MaxWheelSize, m_Genome.Wheel1Radius);
    public Color32 Wheel1Color => m_Genome.Wheel1Color;
    public float Wheel2Radius => Lerp(k_MinWheelSize, k_MaxWheelSize, m_Genome.Wheel2Radius);
    public Color32 Wheel2Color => m_Genome.Wheel2Color;

    public Translator(Genome genome)
    {
        m_Genome = genome;
    }
}
