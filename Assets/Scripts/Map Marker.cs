using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    public static event Action<MapMarker> OnMarkerSelected;
    public static event Action<MapMarker> OnMarkerStartMove;
    public static event Action<MapMarker> OnMarkerInizialized;

    public Sprite MarkerSprite {  get; private set; }

    public Vector3 Position { get; private set; }

    public MapMarkerInfo Info { get; private set; }

    private bool isCursorOnMarker = false;
    private bool mouseDown = false;

    public void LoadMarker(MapMarkerInfo info)
    {
        Info = info;
        SetPosition(info.GetPosition());
        SetSprite();
    }

    public void InizializeMarker(string name, string description, string imagePath)
    {
        Info = new MapMarkerInfo();
        Info.InizializeMarker(name, description, imagePath, Position);
        SetSprite();
        OnMarkerInizialized?.Invoke(this);
    }

    public void SetPosition(Vector3 pos)
    {
        pos.z -= 0.1f;
        transform.position = pos;
        Position = pos;
    }

    public void UpdatePosition(Vector3 pos)
    {
        SetPosition(pos);
        Info.UpdatePosition(Position);
    }

    public void DestroyMarker()
    {
        Destroy(gameObject);
    }

    private void SetSprite()
    {
        MarkerSprite = FilePicker.GetSpriteFromPath(Info.ImagePath);
    }

    private void OnMouseEnter()
    {
        isCursorOnMarker = true;
        StartCoroutine(WaitDelayTimeCoroutine());
    }

    private void OnMouseDown()
    {
        if (!mouseDown)
            mouseDown = true;
    }

    private IEnumerator WaitDelayTimeCoroutine()
    {
        float currentTime = 0f;
        float mouseDownTime = 0f;
        float delayTime = 0.7f;
        bool isDescriptionShowed = false;
        bool canStartMoveMarker = true;

        while (isCursorOnMarker && !isDescriptionShowed
            && canStartMoveMarker)
        {
            if (!mouseDown)
            {
                mouseDownTime = 0f;
                currentTime += Time.deltaTime;

                if (currentTime >= delayTime)
                {
                    isDescriptionShowed = true;
                    OnMarkerSelected?.Invoke(this);
                }
            }

            if (mouseDown)
            {
                currentTime = 0f;
                mouseDownTime += Time.deltaTime;

                if (mouseDownTime >= delayTime)
                {
                    canStartMoveMarker = false;
                    OnMarkerStartMove?.Invoke(this);
                }

            }

            yield return null;
        }
    }

    private void OnMouseUp()
    {
        mouseDown = false;
    }

    private void OnMouseExit()
    {
        mouseDown = false;
        isCursorOnMarker = false;
    }
}
