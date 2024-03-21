using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PathCreator))]
public class TrackRenderer : MonoBehaviour
{
    public PathCreator pathCreator { get; private set; }
    LineRenderer lineRenderer;
    public float trackWidth = 0.3f;

    public void GenerateMesh()
    {
        pathCreator = GetComponent<PathCreator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = trackWidth;
        lineRenderer.endWidth = trackWidth;
        int maxNum = pathCreator.path.NumPoints;
        lineRenderer.positionCount = maxNum;
        Vector3[] nodes = new Vector3[maxNum];
        for (int i = 0; i < maxNum; i++)
        {
            nodes[i] = pathCreator.path.GetPoint(i);
        }
        lineRenderer.SetPositions(nodes);
    }

}
