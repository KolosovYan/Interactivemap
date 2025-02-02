using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    private Vector2 inputMousePos;
    private Vector3 clickPos;
    private PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    private List<RaycastResult> results = new List<RaycastResult>();

    public Vector3 GetClickPosition()
    {
        Vector3 inputPos = Input.mousePosition;
        inputPos.z = -Camera.main.transform.position.z;
        clickPos = Camera.main.ScreenToWorldPoint(inputPos);

        return clickPos;
    }

    public bool IsPointerOverUIObject()
    {
        inputMousePos.x = Input.mousePosition.x;
        inputMousePos.y = Input.mousePosition.y;

        eventDataCurrentPosition.position = inputMousePos;

        results.Clear();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
