using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkerInfoWindow : MonoBehaviour
{
    public static event Action<MapMarker> OnEditPressed;
    public static event Action<MapMarker> OnDeletePressed;

    [Header("Components")]

    [SerializeField] private GameObject cashedGO;
    [SerializeField] private TMP_Text markerName;
    [SerializeField] private TMP_Text markerDescription;
    [SerializeField] private Image markerImage;

    private MapMarker lastMarker;

    public void ShowMarkerInfo(MapMarker marker)
    {
        markerName.text = marker.Info.Name;
        markerDescription.text = marker.Info.Description;
        markerImage.sprite = marker.MarkerSprite;
        lastMarker = marker;
        cashedGO.SetActive(true);
    }

    public void EditMarkerPressed() => OnEditPressed?.Invoke(lastMarker);

    public void DeleteMarkerPressed() => OnDeletePressed?.Invoke(lastMarker);

    public bool IsWindowActive() { return cashedGO.activeSelf; }

    public void HideWindow() => cashedGO.SetActive(false);

}
