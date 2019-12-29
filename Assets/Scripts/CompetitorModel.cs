using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorModel {

    Genome m_Genome;

    List<Vector2> m_JointAnchors = new List<Vector2>();

    public Genome Genome => m_Genome;
    public Translator Translator;
    public BodyGraph BodyGraph;
    public List<int> ConnectedWheels;

    public CompetitorModel(Genome genome)
    {
        m_Genome = genome;
        Translator = new Translator(genome);
        BodyGraph = new BodyGraph();
        ConnectedWheels = new List<int>();
    }

    public void GenerateModel()
    {
        m_JointAnchors.Add(Vector3.right);
        m_JointAnchors.Add(Vector3.left);
    }
}
