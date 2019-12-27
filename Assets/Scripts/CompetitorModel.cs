using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorModel {

    Genome m_Genome;

    List<Vector2> m_Points = new List<Vector2>();
    List<Vector2> m_JointAnchors = new List<Vector2>();

    public Genome Genome => m_Genome;
    public List<Vector2> Points => m_Points;
    public List<Vector2> JointAnchors => m_JointAnchors;
    public Translator Translator;

    public CompetitorModel(Genome genome)
    {
        m_Genome = genome;
        Translator = new Translator(genome);
    }

    public void GenerateModel()
    {
        m_Points.Add(Vector2.zero);
        m_JointAnchors.Add(Vector3.right);
        m_JointAnchors.Add(Vector3.left);
    }

    public Vector2[] GenerateEdgePoints()
    {
        var pts = new Vector2[3];

        pts[0] = Vector2.up;
        pts[1] = Vector2.right;
        pts[2] = Vector2.left;

        return pts;
    }
}
