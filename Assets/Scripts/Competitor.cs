using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour {

    [SerializeField]
    CompetitorPart m_PartTemplate;

    CompetitorModel m_Model;

    public Transform MainBody;

    public void Configure(Genome genome)
    {
        if(MainBody != null)
        {
            Destroy(MainBody.gameObject);
        }

        m_Model = new CompetitorModel(genome);
        m_Model.GenerateModel();

        var cb = new CompetitorBuilder(m_PartTemplate);
        var body = cb.Build(m_Model);
        body.transform.SetParent(transform, false);

        MainBody = body.transform;
    }
}
