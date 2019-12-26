using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour {

    [SerializeField]
    CompetitorPart m_PartTemplate;

    CompetitorModel m_Model;

    Transform m_MainBody;

    public Transform MainBody => m_MainBody;

    public void Configure(Genome genome)
    {
        m_Model = new CompetitorModel(genome);
        m_Model.GenerateModel();

        var cb = new CompetitorBuilder(m_PartTemplate);
        var body = cb.Build(m_Model);
        body.transform.SetParent(transform, false);

        m_MainBody = body.transform;
    }
}
