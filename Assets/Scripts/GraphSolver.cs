using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class BodyGraph
{

    const int k_RootPointCount = 3;

    public struct Extension
    {
        public int edgePointA, edgePointB;
        public float distanceA, distanceB;
    }

    public Vector2[] RootPoints;
    public int[] RootEdges = new[] {
        0,1,
        1,2,
        2,0
    };

    public List<Extension> Extensions = new List<Extension>();
    public Vector2[] Perimeter;

    public BodyGraph()
    {
        var c = 2 * PI;
        RootPoints = new[]{
            new Vector2(2*Sin(c*Random.value/3f), 2*Cos(c*Random.value/3f)),
            new Vector2(2*Sin(c*(1f/3f+Random.value/3f)), 2*Cos(c*(1f/3f+Random.value/3f))),
            new Vector2(2*Sin(c*(2f/3f+Random.value/3f)), 2*Cos(c*(2f/3f+Random.value/3f)))
        };
    }

    public Mesh GetMesh()
    {
        var m = new Mesh();
        var fe = new List<int>();

        var np = k_RootPointCount + Extensions.Count;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = 3 * (Extensions.Count + 1);
        var t = new int[nt];

        v[0] = RootPoints[0];
        v[1] = RootPoints[1];
        v[2] = RootPoints[2];

        n[0] = Vector3.back;
        n[1] = Vector3.back;
        n[2] = Vector3.back;

        t[0] = 0;
        t[1] = 1;
        t[2] = 2;

        fe.Add(0);
        fe.Add(1);
        fe.Add(2);

        for (int i = 0; i < Extensions.Count; i++)
        {
            var e = Extensions[i];
            var p0 = v[e.edgePointA];
            var p1 = v[e.edgePointB];
            var pn = (p1 - p0).normalized;
            pn = new Vector3(-pn.y, pn.x, 0);
            var p2 = Vector3.Lerp(p0, p1, e.distanceA) + pn * e.distanceB;

            v[k_RootPointCount + i] = p2;
            n[k_RootPointCount + i] = Vector3.back;

            var ei = fe.IndexOf(e.edgePointB);
            fe.Insert(ei, k_RootPointCount + i);

            var vi = k_RootPointCount + i * 3;
            t[vi] = e.edgePointA;
            t[vi + 1] = e.edgePointB;
            t[vi + 2] = k_RootPointCount + i;
        }

        m.vertices = v;
        m.normals = n;
        m.triangles = t;

        Perimeter = new Vector2[fe.Count];
        for(int i=0;i<fe.Count;i++)
        {
            Perimeter[i] = v[fe[i]];
        }

        return m;
    }
}
