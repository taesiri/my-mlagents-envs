using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DummyChart : MonoBehaviour
{
    public int ChartLength = 50;
    Queue<float> heights = new Queue<float>();

    public Vector3[] allDataPoints;
    public LineRenderer chartLine;
    public Vector3 offset = Vector3.zero;
    public float stepSize = 1f;
    public void Start()
    {
        // allDataPoints = new List<Vector3>();
        chartLine.positionCount = 0;
    }
    public void AddNewDataPoit(float point)
    {
        heights.Enqueue(point);
        if (heights.Count > ChartLength)
        {
            heights.Dequeue();

            var copyOfQueue = heights.ToArray();

            allDataPoints = new Vector3[ChartLength];

            for (int i = 0; i < copyOfQueue.Length; i++)
            {
                allDataPoints[i] = new Vector3(stepSize * i, copyOfQueue[i], 0) + offset;
            }

            chartLine.positionCount = ChartLength;
            chartLine.SetPositions(allDataPoints);
        }
    }
}
