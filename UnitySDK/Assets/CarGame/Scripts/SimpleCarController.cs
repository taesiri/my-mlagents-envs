using UnityEngine;
using MLAgents;

public class SimpleCarController : Agent
{
    private Vector3 initialPosition;
    private float initial_m_steeringAngle;
    private float initial_frontDriverWsteerAngle;
    private float initial_frontPassengerWsteerAngle;
    private float initial_frontDrivemWmotorTorque;
    private float initial_frontPassengerWmotorTorque;
    private Quaternion InitialCarRotation;
    public Transform TargetT;

    private float initialY;

    public bool DrawCharts = false;
    public DummyChart ChartAccelaration;
    public DummyChart ChartSteering;



    public override void InitializeAgent()
    {

    }

    public override void CollectObservations()
    {
        AddVectorObs(TargetT.position);

        AddVectorObs(gameObject.transform.position);
        AddVectorObs(gameObject.transform.rotation);

        AddVectorObs(m_steeringAngle);
        AddVectorObs(frontDriverW.steerAngle);
        AddVectorObs(frontPassengerW.steerAngle);
        AddVectorObs(frontDriverW.motorTorque);
        AddVectorObs(frontPassengerW.motorTorque);
    }

    public override void AgentAction(float[] vectorAction)
    {
        var actionX = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
        var actionY = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);

        if (DrawCharts)
        {
            ChartAccelaration.AddNewDataPoit(actionX);
            ChartSteering.AddNewDataPoit(actionY);
        }
		
        m_horizontalInput = actionX;
        m_verticalInput = actionY;

        if ((transform.position - TargetT.position).sqrMagnitude < 1f)
        {
            Done();
            SetReward(10f);
        }
        else if (Mathf.Abs(transform.position.y - initialY) > 1f)
        {
            Done();
            SetReward(-10f);
        }
    }

    public override void AgentReset()
    {
        transform.position = initialPosition;

        m_steeringAngle = initial_m_steeringAngle;
        frontDriverW.steerAngle = initial_frontDriverWsteerAngle;
        frontPassengerW.steerAngle = initial_frontPassengerWsteerAngle;
        frontDriverW.motorTorque = initial_frontDrivemWmotorTorque;
        frontPassengerW.motorTorque = initial_frontPassengerWmotorTorque;

        gameObject.transform.rotation = InitialCarRotation;

        TargetT.position = PointInCircle(4.99f, 30f);
    }

    public Vector3 PointInCircle(float r1, float r2)
    {
        var v = Random.onUnitSphere * Random.Range(r1, r2);
        return new Vector3(v.x, initialY, v.z);
    }

    public override float[] Heuristic()
    {
        var action = new float[2];

        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }

    public void GetInput()
    {
        // m_horizontalInput = Input.GetAxis("Horizontal");
        // m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    public void Awake()
    {
        initialPosition = transform.position;

        initial_m_steeringAngle = m_steeringAngle;
        initial_frontDriverWsteerAngle = frontDriverW.steerAngle;
        initial_frontPassengerWsteerAngle = frontPassengerW.steerAngle;
        initial_frontDrivemWmotorTorque = frontDriverW.motorTorque;
        initial_frontPassengerWmotorTorque = frontPassengerW.motorTorque;
        InitialCarRotation = gameObject.transform.rotation;

        initialY = TargetT.position.y;
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
}
