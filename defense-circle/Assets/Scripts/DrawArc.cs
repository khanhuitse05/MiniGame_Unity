using UnityEngine;
using System.Collections;

public class DrawArc : MonoBehaviour {

    LineRenderer line;
    public float radius = 5.0f;
    public int start;
    public int end;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (end > start)
        {
            line.SetVertexCount(end - start + 1);
            for (var i = 0; i < (end - start + 1); i++)
            {
                var angle = (360 / GameConstants.resolution) * (i + start);
                line.SetPosition(i, transform.position + radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
            }
        }
        else
        {
            line.SetVertexCount(GameConstants.resolution - start + end + 1);
            for (var i = 0; i < (GameConstants.resolution - start); i++)
            {
                var angle = (360 / GameConstants.resolution) * (i + start);
                line.SetPosition(i, transform.position + radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
            }
            for (var i = 0; i < end + 1; i++)
            {
                var angle = (360 / GameConstants.resolution) * (i);
                line.SetPosition(GameConstants.resolution - start + i, transform.position + radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
            }
        }
    }
    public bool CheckCollision(int index)
    {
        if (end > start)
        {
            if (index >= start && index <= end)
            {
                return true;
            }
        }
        else
        {

            if (index >= start && index >= end || index <= start && index <= end)
            {
                return true;
            }
        }
        return false;
    }
}
