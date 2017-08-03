using UnityEngine;
using System.Collections;

public class DrawCircles : MonoBehaviour
{
    LineRenderer line;
    public float radius = 5.0f;
    CircleCollider2D collider;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        collider = GetComponent<CircleCollider2D>();
        collider.radius = radius;
    }

    void Update()
    {

        line.SetVertexCount(GameConstants.resolution + 1);

        for (var i = 0; i < GameConstants.resolution + 1; i++)
        {
            var angle = (360 / GameConstants.resolution) * i;
            line.SetPosition(i, transform.position + radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
        }
    }
    public void setRadius(float _r)
    {
        radius = _r;
        collider.radius = radius;
    }
}
