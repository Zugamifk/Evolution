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

        var pn = model.BodyGraph.Perimeter.Length;

        // add wheels
        var wb = new WheelBuilder();
        var wheel = wb.BuildPart(model, m_PartTemplate, model.Translator.Wheel1);
        wheel.transform.SetParent(body.transform);
        var joint = wheel.GetComponent<WheelJoint2D>();
        var rb = body.gameObject.GetComponent<Rigidbody2D>();
        rb.mass = 0.1f;
        joint.connectedBody = rb;
        var wa1 = model.Translator.Wheel1.Anchor % pn;
        joint.connectedAnchor = model.BodyGraph.Perimeter[wa1];

        wheel = wb.BuildPart(model, m_PartTemplate, model.Translator.Wheel2);
        wheel.transform.SetParent(body.transform);
        joint = wheel.GetComponent<WheelJoint2D>();
        rb = body.gameObject.GetComponent<Rigidbody2D>();
        rb.mass = 0.1f;
        joint.connectedBody = rb;
        var wa2 = model.Translator.Wheel2.Anchor % pn;
        if (wa1 == wa2) wa2 = (wa2 + 1) % pn;
        joint.connectedAnchor = model.BodyGraph.Perimeter[wa2];

        return body.gameObject;
    }
}
