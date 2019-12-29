using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Mathf;

public class BodyGraph
{
    const int k_RootPointCount = 3;

    public Vector2[] Perimeter;

    public Mesh GetMesh(Translator translator)
    {
        var m = new Mesh();
        var fe = new List<int>();

        var np = k_RootPointCount + translator.Extensions.Length;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = 3 * (translator.Extensions.Length + 1);
        var t = new int[nt];

        v[0] = translator.RootPoints[0];
        v[1] = translator.RootPoints[1];
        v[2] = translator.RootPoints[2];

        n[0] = Vector3.back;
        n[1] = Vector3.back;
        n[2] = Vector3.back;

        t[0] = 0;
        t[1] = 1;
        t[2] = 2;

        fe.Add(0);
        fe.Add(1);
        fe.Add(2);

        for (int i = 0; i < translator.Extensions.Length; i++)
        {
            var e = translator.Extensions[i];
            var ea = e.edgePoint % fe.Count;
            var eb = (e.edgePoint + 1) % fe.Count;
            if (ea == eb) continue;

            var p0 = v[fe[ea]];
            var p1 = v[fe[eb]];
            var pn = (p1 - p0).normalized;
            pn = new Vector3(-pn.y, pn.x, 0);
            var p2 = Vector3.Lerp(p0, p1, e.distanceA) + pn * e.distanceB*2;

            v[k_RootPointCount + i] = p2;
            n[k_RootPointCount + i] = Vector3.back;

            var vi = k_RootPointCount + i * 3;
            t[vi] = fe[ea];
            t[vi + 1] = k_RootPointCount + i;
            t[vi + 2] = fe[eb];

            fe.Insert(eb, k_RootPointCount + i);
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
