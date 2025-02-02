using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static event Action OnEnterCreationMode;
    public static event Action OnExitCreationMode;

    [Header("Components")]

    [SerializeField] private Map map;
    [SerializeField] private List<MapMarker> markers;

    private bool isMarkerCreationMode = false;

    public void SwitchCreationModeState()
    {
        if (isMarkerCreationMode)
            DisableCreationMode();

        else
            EnableCreationMode();
    }

    public void OnSavePressed() => SaveController.SaveMarkers(markers);

    private void Awake()
    {
        MapMarker.OnMarkerInizialized += AddMarkerToList;
        LoadMarkers();
    }

    private void LoadMarkers()
    {
        markers = new List<MapMarker>(SaveController.LoadMarkers());

        if (markers.Count > 0)
        {
            foreach (MapMarker marker in markers)
                marker.LoadMarker();
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
        isMarkerCreationMode = true;
        Map.OnMapTouched += CreateNewMarker;
        OnEnterCreationMode?.Invoke();
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
        OnExitCreationMode?.Invoke();
    }
}
