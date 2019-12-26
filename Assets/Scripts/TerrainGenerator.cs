using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField]
    EdgeCollider2D m_Collider;
    [SerializeField]
    MeshFilter m_MeshFilter;
    [SerializeField]
    int m_PointCount = 100;

    Vector2[] m_Points;

    private void Start()
    {
        m_Points = new Vector2[m_PointCount];
        for (int i = 0; i < m_PointCount; i++)
        {
            m_Points[i] = new Vector2(i, Random.Range(2, 5));
        }
        m_Collider.points = m_Points;

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

            verts[i0] = new Vector3(i, 0, 0);
            verts[i1] = m_Points[i];

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
