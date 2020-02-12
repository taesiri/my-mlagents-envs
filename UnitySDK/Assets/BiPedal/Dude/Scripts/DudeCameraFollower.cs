using System;
using UnityEngine;

public class DudeCameraFollower : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 _velocity = Vector3.zero;
    public Transform target;
    public Vector3 offset;
    private Camera _followCam;

    private void Start()
    {
        _followCam = GetComponent<Camera>();
    }

    void Update()
    {
        if (target)
        {

            var tagertPoistion = new Vector3(target.position.x, transform.position.y, transform.position.z) + offset;

            Vector3 point = Camera.main.WorldToViewportPoint(tagertPoistion);
            Vector3 delta =
                tagertPoistion -
                Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
        }

    }
}