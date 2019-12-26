using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompetitorPartBuilder
{
    protected CompetitorPart InstantiatePart(CompetitorPart partTemplate)
    {
        var part = GameObject.Instantiate(partTemplate);
        part.gameObject.SetActive(true);
        return part;
    }
}

public class BodyBuilder : CompetitorPartBuilder
{
    Mesh BuildMesh(List<Vector2> points)
    {
        var m = new Mesh();

        var np = points.Count + 2;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = points.Count * 3;
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

    public CompetitorPart BuildPart(CompetitorModel model, CompetitorPart partTemplate)
    {
        var body = InstantiatePart(partTemplate);
        var mesh = BuildMesh(model.Points);
        body.MeshFilter.mesh = mesh;

        var poly = body.gameObject.AddComponent<PolygonCollider2D>();
        poly.points = model.GenerateEdgePoints();

        return body;
    }
}

public class WheelBuilder : CompetitorPartBuilder
{
    Mesh BuildMesh(float radius)
    {
        const int k_WheelPoints = 32;
        var m = new Mesh();

        var np = k_WheelPoints + 1;
        var v = new Vector3[np];
        var n = new Vector3[np];
        var nt = k_WheelPoints * 3;
        var t = new int[nt];

        var step = Mathf.PI * 2 / ((float)k_WheelPoints);
        for (int i = 0; i < k_WheelPoints; i++)
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

    public CompetitorPart BuildPart(CompetitorModel model, CompetitorPart partTemplate, int index)
    {
        var wheel = InstantiatePart(partTemplate);
        var radius = 1f;
        switch (index)
        {
            case 0: radius = model.Genome.Wheel1Radius; break;
            case 1: radius = model.Genome.Wheel2Radius; break;
        }
        var mesh = BuildMesh(radius);
        wheel.MeshFilter.mesh = mesh;
        wheel.MeshFilter.GetComponent<MeshRenderer>().material.color = Color.blue;

        var col = wheel.gameObject.AddComponent<CircleCollider2D>();
        col.radius = radius;

        var joint = wheel.gameObject.AddComponent<WheelJoint2D>();
        joint.enableCollision = false;

        joint.motor = new JointMotor2D() { motorSpeed = 500, maxMotorTorque = 300 };
        joint.suspension = new JointSuspension2D() { dampingRatio = 1, frequency = 100 };

        return wheel;
    }
}
