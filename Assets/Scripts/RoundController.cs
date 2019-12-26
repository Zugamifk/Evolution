using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour {

    [SerializeField]
    Camera m_Camera;
    [SerializeField]
    Competitor m_CompetitorTemplate;

    List<Competitor> m_Competitors = new List<Competitor>();

    private void Start()
    {
        var c = Instantiate(m_CompetitorTemplate);
        c.transform.position = new Vector3(2, 6, 0);
        c.gameObject.SetActive(true);
        m_Competitors.Add(c);
    }

    private void Update()
    {
        m_Camera.transform = 
    }
}
