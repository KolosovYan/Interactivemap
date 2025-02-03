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
    [SerializeField] private GameObject markerMoveMode;
    [SerializeField] private MarkerPopupInfo markerPopupInfo;
    [SerializeField] private MarkerInfoWindow markerInfo;

    private void Awake()
    {
        MapMarker.OnMarkerSelected += ShowPopupMarkerInfo;
        MarkerPopupInfo.OnShowInfoPressed += ShowFullMarkerInfo;
        MapController.OnCreationModeStateChange += SetMarkerAddMode;
        MapController.OnMoveModeStateChange += SetMarkerMoveMode;
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

    private void SetMarkerAddMode(bool state)
    {
        markerAddMode.SetActive(state);
        markerAddModeButton.SetActive(!state);
    }

    private void SetMarkerMoveMode(bool state)
    {
        markerMoveMode.SetActive(state);
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
