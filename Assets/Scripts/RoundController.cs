using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour {

    [SerializeField]
    Camera m_Camera;
    [SerializeField]
    Text m_TimeText;
    [SerializeField]
    Text m_DistanceText;
    [SerializeField]
    Competitor m_CompetitorTemplate;
    [SerializeField]
    int m_TournamentSize;
    [SerializeField]
    float m_MaxSeconds;
    [SerializeField]
    float m_MaxDistance;

    Tournament m_Tournament;
    Competitor m_Competitor;

    float m_StartPosition = 25;

    const string k_TimeFormat = @"m\:ss\.f";
    const string k_DistanceFormat = @"0.##";

    private void Start()
    {
        m_Tournament = new Tournament(m_TournamentSize, m_MaxDistance, m_MaxSeconds);

        var c = Instantiate(m_CompetitorTemplate);
        c.gameObject.SetActive(true);
        m_Competitor = c;

        m_Tournament.StartRound();
        StartTest();
    }

    void StartTest()
    {
        m_Competitor.transform.position = new Vector3(m_StartPosition, 6, 0);
        m_Competitor.Configure(m_Tournament.Current);
    }

    private void Update()
    {
        var p = m_Competitor.MainBody.transform.position;
        p.z = -10;
        m_Camera.transform.position = p;

        m_Tournament.Distance = m_Competitor.MainBody.transform.position.x - m_StartPosition;

        m_TimeText.text = $"<size=36>{m_Tournament.Time.ToString(k_TimeFormat)}</size> / {m_Tournament.MaxTime.ToString(k_TimeFormat)}";
        m_DistanceText.text = $"<size=36>{m_Tournament.Distance.ToString(k_DistanceFormat)}</size> / {m_Tournament.MaxDistance.ToString(k_DistanceFormat)}";

        if (m_Tournament.IsRunning)
        {
            if (m_Tournament.IsRoundOver)
            {
                m_Tournament.StartRound();
                StartTest();
            }
            else if (m_Tournament.IsTestOver)
            {
                m_Tournament.StartTest();
                StartTest();
            }
        }
    }
}
