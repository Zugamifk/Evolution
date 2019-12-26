using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour {

    [SerializeField]
    Camera m_Camera;
    [SerializeField]
    Competitor m_CompetitorTemplate;

    List<Competitor> m_Competitors = new List<Competitor>();
    Competitor m_Watched;

    private void Start()
    {
        var c = Instantiate(m_CompetitorTemplate);
        c.transform.position = new Vector3(25, 6, 0);
        c.gameObject.SetActive(true);
        m_Competitors.Add(c);
        m_Watched = c;
    }

    private void Update()
    {
        var p = m_Watched.MainBody.transform.position;
        p.z = -10;
        m_Camera.transform.position = p;
    }
}
