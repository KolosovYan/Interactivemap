using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static event Action OnMapTouched;

    private bool canCreateMarker = true;
    private bool isClickRegistred = false;

    private void Awake()
    {
        UIManager.OnUIWindowOpened += () => SetCanCreateState(false);
        UIManager.OnUIWindowClosed += () => SetCanCreateState(true);
    }

    private void SetCanCreateState(bool state) => canCreateMarker = state;

    private void OnMouseDown()
    {
        if (canCreateMarker && !isClickRegistred)
        {
            isClickRegistred = true;

            OnMapTouched?.Invoke();

            isClickRegistred = false;
        }
    }
}
