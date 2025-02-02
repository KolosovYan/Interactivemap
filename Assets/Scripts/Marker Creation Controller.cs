using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MarkerCreationController : MonoBehaviour
{
    public static MarkerCreationController Instance;
    public static event Action<MapMarker> OnNewMarkerCreated;

    [Header("Components")]

    [SerializeField] private MapMarker prefab;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField descriptionInput;

    private MapMarker lastCreated;

    public void CreateNewMarker(Vector3 markerPosition)
    {
        MapMarker marker = Instantiate(prefab);
        marker.SetPosition(markerPosition);
        OnNewMarkerCreated?.Invoke(marker);
        lastCreated = marker;
    }

    public MapMarker GetLastCreatedMarker() { return lastCreated; }

    public void DestroyLastMarker() => lastCreated.DestroyMarker();

    private void Awake()
    {
        Instance = this;
    }
}
