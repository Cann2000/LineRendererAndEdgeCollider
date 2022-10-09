using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject CurrentLine;
    public GameObject ball;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;

    public List<Vector2> fingerPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // mouseposu sahneye uyarla

            if (Vector2.Distance(tempFingerPos, fingerPos[fingerPos.Count - 1]) > 1f)
            {
                UpdateLine(tempFingerPos);

            }
        }
    }


    void CreateLine()
    {
        CurrentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = CurrentLine.GetComponent<LineRenderer>();
        edgeCollider = CurrentLine.GetComponent<EdgeCollider2D>();

        fingerPos.Clear();

        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));


        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);

        edgeCollider.points = fingerPos.ToArray();

        for (int i = fingerPos.Count - 1; i < fingerPos.Count; i++)
        {
            GameObject Ball = Instantiate(ball, new Vector2(fingerPos[i].x, fingerPos[i].y), Quaternion.identity);
        }
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        edgeCollider.points = fingerPos.ToArray();

        for (int i = fingerPos.Count - 1; i < fingerPos.Count; i++)
        {
            GameObject Ball = Instantiate(ball, new Vector2(fingerPos[i].x, fingerPos[i].y), Quaternion.identity);
        }
    }
}
