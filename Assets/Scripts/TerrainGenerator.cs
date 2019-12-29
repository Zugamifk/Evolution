using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField]
    EdgeCollider2D m_Collider;
    [SerializeField]
    MeshFilter m_MeshFilter;
    [SerializeField]
    int m_PointCount = 100;
    [SerializeField]
    float m_Variance = .4f;
    [SerializeField]
    float m_MaxHeight = 5;

    float[] m_Points;

    private void Start()
    {
        m_Points = new float[m_PointCount];
        var surface = new Vector2[m_PointCount];
        for (int i = 0; i < m_PointCount; i++)
        {
            if (i == 0)
            {
                m_Points[i] = 2;
            }
            else
            {
                m_Points[i] = Mathf.Clamp(m_Points[i - 1] + Random.Range(-m_Variance, m_Variance), 0, m_MaxHeight);
            }
            surface[i] = new Vector2(i, m_Points[i]);
        }
        m_Collider.points = surface;

        UpdateMesh();
    }

    void UpdateMesh()
    {
        var m = new Mesh();
        var vcnt = m_PointCount * 2;
        var verts = new Vector3[vcnt];
        var norms = new Vector3[vcnt];
        var tris = new int[(m_PointCount - 1) * 2 * 3];

        for (int i = 0; i < m_PointCount; i++)
        {
            var i0 = i * 2;
            var i1 = i0 + 1;

            verts[i0] = new Vector3(i, -25, 0);
            verts[i1] = new Vector3(i, m_Points[i], 0);

            norms[i0] = Vector3.back;
            norms[i1] = Vector3.back;

            if (i > 0)
            {
                var t = (i - 1) * 6;
                tris[t] = i0 - 2;
                tris[t + 1] = i1 - 2;
                tris[t + 2] = i1;

                tris[t + 3] = i0 - 2;
                tris[t + 4] = i1;
                tris[t + 5] = i0;
            }
        }

        m.vertices = verts;
        m.normals = norms;
        m.triangles = tris;
        m_MeshFilter.mesh = m;
    }
}
