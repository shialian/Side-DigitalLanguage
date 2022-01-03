using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManipulationOnScreen : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 mouseWorldPosition = GetMousePositionInWorldCoordinate();
            PlayerMovement.singleton.SetNewTarget(mouseWorldPosition);
        }
    }

    private Vector3 GetMousePositionInWorldCoordinate()
    {
        Vector3 defaultPosition = transform.position;
        Vector3 mouseScreenPosition = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mouseScreenPosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return defaultPosition;
    }
}
