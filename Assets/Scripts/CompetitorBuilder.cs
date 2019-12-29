using UnityEngine;

public class CompetitorBuilder
{

    CompetitorPart m_PartTemplate;

    public CompetitorBuilder(CompetitorPart partTemplate)
    {
        m_PartTemplate = partTemplate;
    }

    public GameObject Build(CompetitorModel model)
    {
        var bb = new BodyBuilder();
        var body = bb.BuildPart(model, m_PartTemplate);

        var wb = new WheelBuilder();
        var wc = Mathf.Min(model.Translator.Wheels.Length, model.BodyGraph.Perimeter.Length);
        for (int i = 0; i < wc; i++)
        {
            AddWheel(model.Translator.Wheels[i], wb, body.transform, model);
        }

        return body.gameObject;
    }

    void AddWheel(Genome.Wheel wheel, WheelBuilder builder, Transform body, CompetitorModel model)
    {
        var part = builder.BuildPart(m_PartTemplate, wheel);
        part.transform.SetParent(body.transform);

        var joint = part.GetComponent<WheelJoint2D>();
        var rb = body.gameObject.GetComponent<Rigidbody2D>();
        rb.mass = 0.1f;
        joint.connectedBody = rb;

        var pn = model.BodyGraph.Perimeter.Length;
        int wa = (int)wheel.Anchor % pn;
        while (model.ConnectedWheels.Contains(wa))
        {
            wa = (wa + 1) % pn;
        }
        joint.connectedAnchor = model.BodyGraph.Perimeter[wa];
        model.ConnectedWheels.Add(wa);
    }
}
