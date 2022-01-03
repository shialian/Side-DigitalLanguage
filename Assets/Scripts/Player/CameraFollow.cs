using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private Transform player;

    public float distanceToPlayer = 2.5f;
    public float scrollSpeed = 1.0f;
    public float scrollUpperBound = 3.0f;
    public float scrollLowerBound = 1.0f;

    public float yOffset = 1.0f;
    private Vector3 lookDirection;

    public float rotationSpeedFactor = 5.0f;
    private Vector2 defaultRotationAngle = new Vector2(45f, 0f);
    private Vector2 rotationAngles;
    public float maxVerticalRotation = 89f;
    public float minVerticalRotation = -89f;
    private Quaternion lookRotation;

    public string mouseXInput = "Mouse X";
    public string mouseYInput = "Mouse Y";
    public string mouseScrollWheel = "Mouse ScrollWheel";
    private float mouseX;
    private float mouseY;
    private float mouseScroll;

    private Camera cam;

    private void Start()
    {
        InitObjects();
        UpdateLookInformation();
        UpdateTransform();
    }

    private void InitObjects()
    {
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rotationAngles = defaultRotationAngle;
    }

    private void LateUpdate()
    {
        UpdateInput();
        if (mouseScroll != 0)
        {
            SetLookAtDistance();
        }
        InputEventHandling();
        UpdateTransform();
    }

    private void InputEventHandling()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            UpdateLookInformation();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            defaultRotationAngle = rotationAngles;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotationAngles = defaultRotationAngle;
            UpdateLookInformation();
        }
    }

    private void UpdateInput()
    {
        mouseX = Input.GetAxis(mouseXInput);
        mouseY = Input.GetAxis(mouseYInput);
        mouseScroll = Input.GetAxis(mouseScrollWheel);
    }

    private void SetLookAtDistance()
    {
        distanceToPlayer -= mouseScroll * scrollSpeed;
        distanceToPlayer = Mathf.Clamp(distanceToPlayer, scrollLowerBound, scrollUpperBound);
    }

    private void UpdateLookInformation()
    {
        UpdateLookRotation();
        UpdateLookDirection();
    }

    private void UpdateLookRotation()
    {
        Vector2 mouseInput = new Vector2(-mouseY, mouseX);
        rotationAngles += rotationSpeedFactor * mouseInput * Time.unscaledDeltaTime;
        rotationAngles.x = Mathf.Clamp(rotationAngles.x, minVerticalRotation, maxVerticalRotation);
        lookRotation = Quaternion.Euler(rotationAngles);
    }

    private void UpdateLookDirection()
    {
        lookDirection = lookRotation * Vector3.forward;
    }

    private void UpdateTransform()
    {
        UpdateDistanceToPlayer();
        UpdateRotation();
        UpdatePosition();
    }

    

    private void UpdateDistanceToPlayer()
    {
        RaycastHit hit = CameraBoxCast();
        if (hit.distance < distanceToPlayer)
        {
            distanceToPlayer = hit.distance;
        }
    }

    private RaycastHit CameraBoxCast()
    {
        float distance = 10f;
        Vector3 cameraHalfExtends = GetCameraHalfExtends();
        RaycastHit hit;
        Physics.BoxCast(player.position, cameraHalfExtends, -lookDirection, out hit, lookRotation, distance - cam.nearClipPlane);
        return hit;
    }

    private void UpdateRotation()
    {
        transform.rotation = lookRotation;
    }

    private void UpdatePosition()
    {
        transform.position = player.position - distanceToPlayer * lookDirection;
    }

    private Vector3 GetCameraHalfExtends()
    {
        Vector3 halfExtends;
        halfExtends.y = cam.nearClipPlane * Mathf.Tan(0.5f * Mathf.Deg2Rad * cam.fieldOfView);
        halfExtends.x = halfExtends.y * cam.aspect;
        halfExtends.z = 0f;
        return halfExtends;
    }
}
