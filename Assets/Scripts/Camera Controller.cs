using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.5f, 6f)]
    [SerializeField] private float multiplyer;
    private Transform cashedTransform;
    private Vector3 offset;
    private Vector3 dragOrigin;
    private Vector3 currentMousePosition;
    private Vector3 difference;
    private Vector3 tempVector;
    private float borderValue;
    private bool isDragging = false;
    private bool canMoveCamera = true;


    private void Awake()
    {
        cashedTransform = transform;
        UIManager.OnUIWindowOpened += () => SetCanMoveState(false);
        UIManager.OnUIWindowClosed += () => SetCanMoveState(true);
    }

    private void SetCanMoveState(bool state) => canMoveCamera = state;

    private void Update()
    {
        if (canMoveCamera)
        {
            MoveCamera(Input.GetAxis("Mouse ScrollWheel"));

            if (Input.GetMouseButtonDown(2))
            {
                dragOrigin = InputHandler.Instance.GetClickPosition();
                isDragging = true;
            }

            if (Input.GetMouseButtonUp(2))
            {
                isDragging = false;
            }

            if (isDragging)
            {
                currentMousePosition = InputHandler.Instance.GetClickPosition();
                difference = dragOrigin - currentMousePosition;

                borderValue = CalculateBorderValue();

                tempVector = cashedTransform.position + difference;
                tempVector.x = Mathf.Clamp(tempVector.x, -borderValue, borderValue);
                tempVector.y = Mathf.Clamp(tempVector.y, -borderValue, borderValue);
                cashedTransform.position = tempVector;
            }
        }
    }

    private float CalculateBorderValue()
    {
        return (0.6f * cashedTransform.position.z + 42);
    }

    private void MoveCamera(float offsetZ)
    {
        offset = Vector3.zero;
        offset.z = offsetZ * multiplyer;

        offset.x = cashedTransform.position.x;
        offset.y = cashedTransform.position.y;
        offset.z = Mathf.Clamp((cashedTransform.position.z + offset.z), -70f, -20f);

        cashedTransform.position = offset;
    }
}
