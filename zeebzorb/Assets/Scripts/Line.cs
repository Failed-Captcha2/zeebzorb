using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Line : MonoBehaviour
{
    public LineRenderer penRenderer;
    List<Vector2> points;

    void Start()
    {
        
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);

        penRenderer.positionCount = points.Count;
        penRenderer.SetPosition(points.Count - 1, point);
    }

    public void UpdateLine(Vector2 position)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(points.Last(), position)> 0.1f)
        {
            SetPoint(position);
        }
    }
}
