using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorModel {

    List<Vector2> m_Points = new List<Vector2>();

    public void GenerateModel()
    {
        m_Points.Add(Vector2.zero);
    }

    public Mesh GenerateMesh()
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
