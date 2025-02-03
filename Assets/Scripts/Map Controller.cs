using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static event Action<bool> OnCreationModeStateChange;
    public static event Action<bool> OnMoveModeStateChange;

    [Header("Components")]

    [SerializeField] private MarkerCreationController markerCreationController;
    [SerializeField] private List<MapMarker> markers;

    private bool isMarkerCreationMode = false;
    private bool isMarkerMoveMode = false;
    private MapMarker movableMarker;

    public void SwitchCreationModeState()
    {
        if (isMarkerCreationMode)
            DisableCreationMode();

        else
            EnableCreationMode();
    }

    public void EnableMarkerMoveMode(MapMarker m)
    {
        isMarkerMoveMode = true;
        movableMarker = m;

        if (isMarkerCreationMode)
            DisableCreationMode();

        Map.OnMapTouched += MoveMarker;
        OnMoveModeStateChange?.Invoke(true);
    }

    public void DisableMarkerMoveMode()
    {
        Map.OnMapTouched -= MoveMarker;
        movableMarker = null;
        OnMoveModeStateChange?.Invoke(false);
    }

    private void MoveMarker()
    {
        if (movableMarker != null && !InputHandler.Instance.IsPointerOverUIObject())
        {
            movableMarker.UpdatePosition(InputHandler.Instance.GetClickPosition());
            DisableMarkerMoveMode();
        }
    }

    public void OnSavePressed()
    {
        List<MapMarkerInfo> markersInfo = new List<MapMarkerInfo>();

        for (int i = 0; i < markers.Count; i++)
        {
            markersInfo.Add(markers[i].Info);
        }

        SaveController.SaveMarkers(markersInfo);
    }

    private void Awake()
    {
        MapMarker.OnMarkerInizialized += AddMarkerToList;
        MapMarker.OnMarkerStartMove += EnableMarkerMoveMode;
        MarkerInfoWindow.OnDeletePressed += DeleteMarker;
        LoadMarkers();
    }

    private void LoadMarkers()
    {
        List<MapMarkerInfo> markersInfo = SaveController.LoadMarkers();
        markers = new List<MapMarker>();

        if (markersInfo != null)
        {
            if (markersInfo.Count > 0)
            {
                for (int i = 0; i < markersInfo.Count; i++)
                {
                    markersInfo[i].Log();
                    MapMarker tempMarker = markerCreationController.CreateMarkerFromInfo(markersInfo[i]);
                    markers.Add(tempMarker);
                }
            }
        }
    }

    private void AddMarkerToList(MapMarker marker)
    {
        if (!markers.Contains(marker))
            markers.Add(marker);
    }

    private void DeleteMarker(MapMarker marker)
    {
        markers.Remove(marker);
        marker.DestroyMarker();
    }

    private void EnableCreationMode()
    {
        if (isMarkerMoveMode)
            DisableMarkerMoveMode();

        isMarkerCreationMode = true;
        Map.OnMapTouched += CreateNewMarker;
        OnCreationModeStateChange?.Invoke(true);
    }

    private void CreateNewMarker()
    {
        if (!InputHandler.Instance.IsPointerOverUIObject())
            MarkerCreationController.Instance.CreateNewMarker(InputHandler.Instance.GetClickPosition());
    }

    private void DisableCreationMode()
    {
        isMarkerCreationMode = false;
        Map.OnMapTouched -= CreateNewMarker;
        OnCreationModeStateChange?.Invoke(false);
    }
}
