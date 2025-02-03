using UnityEngine;

public class MapMarkerInfo 
{
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }

    public void InizializeMarker(string name, string description, string imagePath, Vector3 pos)
    {
        Name = name;
        Description = description;
        ImagePath = imagePath;
        UpdatePosition(pos);
    }

    public void UpdatePosition(Vector3 pos)
    {
        X = pos.x;
        Y = pos.y;
        Z = pos.z;
    }

    public Vector3 GetPosition() { return new Vector3(X, Y, Z);}

    public void Log()
    {
        Debug.Log($"{Name}, {Description}, {ImagePath}");
    }
}
