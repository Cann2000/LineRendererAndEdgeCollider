using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    GameObject CurrentLine;

    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;
    Rigidbody2D rb2D;

    public List<Vector2> fingerPos;


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
        if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.useWorldSpace = false;
            rb2D.simulated = true;


        }
    }


    void CreateLine()
    {
        CurrentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = CurrentLine.GetComponent<LineRenderer>();
        edgeCollider = CurrentLine.GetComponent<EdgeCollider2D>();
        rb2D = CurrentLine.GetComponent<Rigidbody2D>();

        fingerPos.Clear();

        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPos.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));


        lineRenderer.SetPosition(0, fingerPos[0]);
        lineRenderer.SetPosition(1, fingerPos[1]);

        edgeCollider.points = fingerPos.ToArray();

    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPos.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);

        edgeCollider.points = fingerPos.ToArray();

    }
}
