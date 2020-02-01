using UnityEngine;
using MLAgents;

public class DrawerAgent : Agent
{
    public float Radius = 4f;
    public int CircleResolution = 150;
    public LineRenderer MainLine;
    public Vector3[] GeneratedPoints;

    public int RoundCounter = 0;

    private Vector2 LastVector;

    public override void InitializeAgent()
    {
        GeneratedPoints = new Vector3[CircleResolution];
        LastVector = Vector2.zero;

        for (int i = 0; i < CircleResolution; i++)
        {
            GeneratedPoints[i] = Vector3.zero;
        }
    }
    public override void CollectObservations()
    {
        AddVectorObs(Radius);
        AddVectorObs(LastVector);
    }

    public override void AgentAction(float[] vectorAction)
    {
        var generatedX = Radius * Mathf.Clamp(vectorAction[0], -1f, 1f);
        var generatedY = Radius * Mathf.Clamp(vectorAction[1], -1f, 1f);

        if (RoundCounter >= CircleResolution)
        {
            Done();
            var r = CalculateReward();
            SetReward(r);
        }
        else
        {
            GeneratedPoints[RoundCounter] = new Vector3(generatedX, generatedY, 0);
            LastVector = new Vector2(generatedX, generatedY);
            RoundCounter++;
            UpdateLineRederer();
        }
    }

    public void UpdateLineRederer()
    {
        if (RoundCounter > 1)
        {
            MainLine.positionCount = GeneratedPoints.Length;
            MainLine.SetPositions(GeneratedPoints);
        }
        else
        {
            MainLine.positionCount = 0;
        }
    }
    public override void AgentReset()
    {
        LastVector = Vector2.zero;
        GeneratedPoints = new Vector3[CircleResolution];
        for (int i = 0; i < CircleResolution; i++)
        {
            GeneratedPoints[i] = Vector3.zero;
        }
        RoundCounter = 0;
        UpdateLineRederer();
    }
    public float CalculateReward()
    {
        float reward = 0f;
        var sectionAngle = Mathf.PI * 2f / (float)(CircleResolution);

        for (int i = 0; i < CircleResolution; i++)
        {
            var x = Mathf.Cos(i * sectionAngle) * Radius;
            var y = Mathf.Sin(i * sectionAngle) * Radius;

            reward += (new Vector3(x, y, 0) - GeneratedPoints[i]).sqrMagnitude;
        }
        return -Mathf.Sqrt(reward);
    }

    public override float[] Heuristic()
    {
        var action = new float[2];

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

    public void DrawACircle()
    {
        GeneratedPoints = new Vector3[CircleResolution];

        var sectionAngle = Mathf.PI * 2f / (float)(CircleResolution);

        for (int i = 0; i < CircleResolution; i++)
        {
            var x = Mathf.Cos(i * sectionAngle) * Radius;
            var y = Mathf.Sin(i * sectionAngle) * Radius;

            GeneratedPoints[i] = new Vector3(x, y, 0);
        }
        UpdateLineRederer();
    }

    private void Awake()
    {
        // DrawACircle();
    }

}
