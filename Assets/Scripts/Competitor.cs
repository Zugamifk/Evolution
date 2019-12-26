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
        m_Model.GenerateModel();

        var part = Instantiate(m_PartTemplate);
        m_MainBody = part.transform;
        part.transform.SetParent(transform, false);

        var mesh = m_Model.GenerateMesh();
        part.MeshFilter.mesh = mesh;

        var poly = part.gameObject.AddComponent<PolygonCollider2D>();
        poly.points = m_Model.GenerateEdgePoints();
        part.Collider = poly;

        part.gameObject.SetActive(true);
    }
}
