using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundController : MonoBehaviour {

    const float k_StartPosition = 25;

    [SerializeField]
    Camera m_Camera;
    [SerializeField]
    Text m_TimeText;
    [SerializeField]
    Text m_DistanceText;
    [SerializeField]
    Text m_IndividualText;
    [SerializeField]
    Text m_GenerationText;
    [SerializeField]
    Competitor m_CompetitorTemplate;
    [SerializeField]
    int m_TournamentSize;
    [SerializeField]
    float m_MaxSeconds;
    [SerializeField]
    float m_MaxDistance;
    [SerializeField]
    TerrainGenerator m_Terrain;

    Tournament m_Tournament;
    Competitor[] m_Competitors;
    int m_Watched;

    const string k_TimeFormat = @"m\:ss\.f";
    const string k_DistanceFormat = @"0.##";

    private void Start()
    {
        Time.timeScale = 2;
        m_Tournament = new Tournament(m_TournamentSize, m_MaxDistance, m_MaxSeconds);

        m_Competitors = new Competitor[m_Tournament.States.Length];
        for (int i = 0; i < m_Tournament.States.Length; i++)
        {
            var c = Instantiate(m_CompetitorTemplate);
            c.gameObject.SetActive(true);
            m_Competitors[i] = c;
        }

        StartRound();
    }

    void StartRound()
    {
        m_Terrain.Generate();
        m_Tournament.StartRound();
        for(int i=0;i<m_Tournament.States.Length;i++)
        {
            m_Competitors[i].Configure(m_Tournament.Population.Individuals[i].Genome);
            m_Competitors[i].MainBody.transform.position = new Vector3(k_StartPosition, m_Terrain.MaxHeight + 3, 0);
            m_Competitors[i].gameObject.SetActive(true);
            m_Tournament.States[i].Initialize(k_StartPosition);
        }

        m_GenerationText.text = $"<size=36>Generation {m_Tournament.Generation}</size>";
    }

    private void Update()
    {
        int running = 0;
        float farthest = 0;
        for(int i=0;i<m_Tournament.States.Length;i++)
        {
            var s = m_Tournament.States[i];
            if (s.IsRunning)
            {
                running++;
                if(s.Distance > farthest)
                {
                    farthest = s.Distance;
                    m_Watched = i;
                }
                s.Update(m_Competitors[i].MainBody.transform.position.x);
            }

            if(!s.IsRunning)
            {
                m_Competitors[i].gameObject.SetActive(false);
            }
        }

        if (!m_Tournament.IsRunning) StartRound();

        m_IndividualText.text = $"<size=36>{running}</size> / {m_TournamentSize} individuals";
        m_TimeText.text = $"<size=36>{m_Tournament.Time.ToString(k_TimeFormat)}</size> / {m_Tournament.MaxTime.ToString(k_TimeFormat)}";
        m_DistanceText.text = $"<size=36>Best: {farthest.ToString(k_DistanceFormat)}</size> / {m_Tournament.MaxDistance.ToString(k_DistanceFormat)}";

        var pos = m_Competitors[m_Watched].MainBody.transform.position;
        pos.z = -10;
        m_Camera.transform.position = pos;
    }
}
