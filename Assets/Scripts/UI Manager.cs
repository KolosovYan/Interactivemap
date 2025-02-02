using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static event Action OnUIWindowOpened;
    public static event Action OnUIWindowClosed;

    [Header("Components")]

    [SerializeField] private MarkerCreationWindow markerCreationWindow;
    [SerializeField] private GameObject markerAddMode;
    [SerializeField] private GameObject markerAddModeButton;
    [SerializeField] private MarkerPopupInfo markerPopupInfo;
    [SerializeField] private MarkerInfoWindow markerInfo;

    private void Awake()
    {
        MapMarker.OnMarkerSelected += ShowPopupMarkerInfo;
        MarkerPopupInfo.OnShowInfoPressed += ShowFullMarkerInfo;
        MapController.OnEnterCreationMode += EnableMarkerAddMode;
        MapController.OnExitCreationMode += DisableMarkerAddMode;
        MarkerCreationController.OnNewMarkerCreated += (m) => OpenMarkerCreationWindow(m);
        MarkerInfoWindow.OnEditPressed += (m) => OpenMarkerCreationWindow(m, true);
    }

    public void OnCloseWindowPressed()
    {
        OnUIWindowClosed?.Invoke();
    }

    private void ShowPopupMarkerInfo(MapMarker marker)
    {
        markerPopupInfo.ShowMarkerInfo(marker);
    }

    private void ShowFullMarkerInfo(MapMarker marker)
    {
        markerPopupInfo.HideWindow();
        markerInfo.ShowMarkerInfo(marker);
        OnUIWindowOpened?.Invoke();
    }

    private void EnableMarkerAddMode()
    {
        markerAddMode.SetActive(true);
        markerAddModeButton.SetActive(false);
    }

    private void DisableMarkerAddMode()
    {
        markerAddMode.SetActive(false);
        markerAddModeButton.SetActive(true);
    }

    private void OpenMarkerCreationWindow(MapMarker m, bool editMode = false)
    {
        if (markerPopupInfo.IsWindowActive())
            markerPopupInfo.HideWindow();

        if (markerInfo.IsWindowActive())
            markerInfo.HideWindow();
        if (!editMode)
            markerCreationWindow.OpenCreationWindow();
        else
            markerCreationWindow.OpenCreationWindow(m);

        OnUIWindowOpened?.Invoke();
    }
}
