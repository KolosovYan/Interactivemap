using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkerPopupInfo : MonoBehaviour
{
    public static event Action<MapMarker> OnShowInfoPressed;

    [Header("Components")]

    [SerializeField] private GameObject cashedGO;
    [SerializeField] private TMP_Text markerName;
    [SerializeField] private TMP_Text markerDescription;
    [SerializeField] private Image markerImage;

    private MapMarker lastSelected;

    public void ShowMarkerInfo(MapMarker marker)
    {
        markerName.text = marker.Name;
        markerDescription.text = LimitTextToTenWords(marker.Description);
        markerImage.sprite = marker.MarkerSprite;
        lastSelected = marker;
        cashedGO.SetActive(true);
    }

    public void ShowFullInfo()
    {
        OnShowInfoPressed?.Invoke(lastSelected);
    }

    public bool IsWindowActive() { return cashedGO.activeSelf; }

    public void HideWindow() => cashedGO.SetActive(false);

    private string LimitTextToTenWords(string fullText)
    {
        string[] words = fullText.Split(' ');

        string firstTenWords = string.Join(" ", words.Take(10).ToArray());

        if (words.Length > 10)
        {
            firstTenWords += "...";
        }

        return firstTenWords;
    }
}

