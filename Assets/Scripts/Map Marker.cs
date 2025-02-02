using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{
    public static event Action<MapMarker> OnMarkerSelected;
    public static event Action<MapMarker> OnMarkerInizialized;

    public string Name {  get; private set; }
    public string Position { get; private set; }
    public string Description { get; private set; }
    public string ImagePath { get; private set; }

    private bool isCursorOnMarker = false;
    private bool mouseDown = false;

    public void LoadMarker()
    {
        SetPosition(Vector3Converter.StringToVector3(Position));
        SetSprite();
    }

    public void InizializeMarker(string name, string description, string imagePath)
    {
        Name = name;
        Description = description;
        ImagePath = imagePath;
        SetSprite();
        OnMarkerInizialized?.Invoke(this);
    }

    public void SetPosition(Vector3 pos)
    {
        pos.z -= 0.1f;
        transform.position = pos;
        Position = Vector3Converter.Vector3ToString(pos);
    }

    public void DestroyMarker()
    {
        Destroy(gameObject);
    }

    private void SetSprite()
    {
        MarkerSprite = FilePicker.GetSpriteFromPath(ImagePath);
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

        while (isCursorOnMarker && !isDescriptionShowed)
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
                    //Move Mode;
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
