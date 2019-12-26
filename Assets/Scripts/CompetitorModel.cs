using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorModel {

    List<Vector2> m_Points = new List<Vector2>();
    List<Vector2> m_JointAnchors = new List<Vector2>();

    public List<Vector2> JointAnchors => m_JointAnchors;

    public void GenerateModel()
    {
        m_Points.Add(Vector2.zero);
    }

    public Mesh GenerateBody()
    {
        var m = new Mesh();

        var np = m_Points.Count + 2;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = m_Points.Count * 3;
        var t = new int[nt];

        v[0] = Vector3.up;
        v[1] = Vector3.right;
        v[2] = Vector3.left;

        n[0] = Vector3.back;
        n[1] = Vector3.back;
        n[2] = Vector3.back;

        t[0] = 0;
        t[1] = 1;
        t[2] = 2;

        m.vertices = v;
        m.normals = n;
        m.triangles = t;

        m_JointAnchors.Add(v[1]);
        m_JointAnchors.Add(v[2]);

        return m;
    }

    public Mesh GenerateWheel(float radius)
    {
        const int k_WheelPoints = 32;
        var m = new Mesh();

        var np = k_WheelPoints + 1;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = k_WheelPoints*3;
        var t = new int[nt];

        var step = Mathf.PI * 2 / ((float)k_WheelPoints);
        for(int i=0;i< k_WheelPoints;i++)
        {
            v[i] = new Vector3(radius * Mathf.Sin(step * i), radius * Mathf.Cos(step * i), 0);
            n[i] = Vector3.back;

            var ti = i * 3;
            t[ti] = k_WheelPoints;
            t[ti + 1] = i;
            t[ti + 2] = (i + 1) % k_WheelPoints;
        }

        v[k_WheelPoints] = Vector3.zero;
        n[k_WheelPoints] = Vector3.back;

        m.vertices = v;
        m.normals = n;
        m.triangles = t;

        return m;
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
