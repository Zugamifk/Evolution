using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour {

    [SerializeField]
    CompetitorPart m_PartTemplate;

    CompetitorModel m_Model = new CompetitorModel();

    Transform m_MainBody;

    public Transform MainBody => m_MainBody;

    private void Start()
    {
        GenerateBody();
    }

    public void GenerateBody()
    {
        m_Model.GenerateModel();

        // body
        var body = InstantiatePart();
        var mesh = m_Model.GenerateBody();
        body.MeshFilter.mesh = mesh;

        var poly = body.gameObject.AddComponent<PolygonCollider2D>();
        poly.points = m_Model.GenerateEdgePoints();

        AddWheel(body, .5f, m_Model.JointAnchors[0]);
        AddWheel(body, .5f, m_Model.JointAnchors[1]);
    }

    CompetitorPart InstantiatePart()
    {
        var part = Instantiate(m_PartTemplate);
        m_MainBody = part.transform;
        part.transform.SetParent(transform, false);
        part.gameObject.SetActive(true);
        return part;
    }

    void AddWheel(CompetitorPart body, float radius, Vector2 anchor)
    {
        var wheel = InstantiatePart();
        var mesh = m_Model.GenerateWheel(radius);
        wheel.MeshFilter.mesh = mesh;
        wheel.MeshFilter.GetComponent<MeshRenderer>().material.color = Color.blue;

        var col = wheel.gameObject.AddComponent<CircleCollider2D>();
        col.radius = radius;

        var joint = wheel.gameObject.AddComponent<WheelJoint2D>();
        joint.enableCollision = false;
        joint.connectedBody = body.gameObject.GetComponent<Rigidbody2D>();
        joint.connectedAnchor = anchor;

        joint.motor = new JointMotor2D() { motorSpeed = 500, maxMotorTorque = 500 };
        joint.suspension = new JointSuspension2D() { dampingRatio = 1, frequency = 100 };
    }
}
