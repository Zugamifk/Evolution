using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorBuilder {

    CompetitorPart m_PartTemplate;

    public CompetitorBuilder(CompetitorPart partTemplate)
    {
        m_PartTemplate = partTemplate;
    }

    public GameObject Build(CompetitorModel model)
    {
        var bb = new BodyBuilder();
        var body = bb.BuildPart(model, m_PartTemplate);

        // add wheels
        var wb = new WheelBuilder();
        var wheel = wb.BuildPart(model, m_PartTemplate, 0);
        wheel.transform.SetParent(body.transform);
        var joint = wheel.GetComponent<WheelJoint2D>();
        joint.connectedBody = body.gameObject.GetComponent<Rigidbody2D>();
        joint.connectedAnchor = model.BodyGraph.Perimeter[Random.Range(0, model.BodyGraph.Perimeter.Length)];

        wheel = wb.BuildPart(model, m_PartTemplate, 1);
        wheel.transform.SetParent(body.transform);
        joint = wheel.GetComponent<WheelJoint2D>();
        joint.connectedBody = body.gameObject.GetComponent<Rigidbody2D>();
        joint.connectedAnchor = model.BodyGraph.Perimeter[Random.Range(0, model.BodyGraph.Perimeter.Length)];

        return body.gameObject;
    }
}
