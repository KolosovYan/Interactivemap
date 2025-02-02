using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveController : MonoBehaviour
{
    public static void SaveMarkers(List<MapMarker> markers)
    {
        string json = JsonConvert.SerializeObject(markers, Formatting.Indented);
        string path = Path.Combine(Application.persistentDataPath, "save.json");
        File.WriteAllText(path, json);
    }

    public static List<MapMarker> LoadMarkers()
    {
        List<MapMarker> markers = new List<MapMarker>();
        string path = Path.Combine(Application.persistentDataPath, "save.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            markers = JsonConvert.DeserializeObject<List<MapMarker>>(json);
        }

        return markers;
    }
}
