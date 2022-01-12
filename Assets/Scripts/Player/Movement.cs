using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement singleton;

    public float speed = 5f;
    private Vector3 targetPosition;
    private float e = 0.001f;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Start()
    {
        singleton = this;
    }

    private void FixedUpdate()
    {
        if(IsTargetReached() == false)
        {
            Moving();
        }
    }

    private bool IsTargetReached()
    {
        return (targetPosition - transform.position).sqrMagnitude <= e;
    }

    private void Moving()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f;
        transform.position += speed * direction * Time.fixedDeltaTime;
    }

    public void SetNewTarget(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }
}
