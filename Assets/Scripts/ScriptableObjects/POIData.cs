using UnityEngine;

[CreateAssetMenu(fileName = "New POI", menuName = "Map/Point of Interest")]
public class POIData : ScriptableObject

{
    // Private data and use getter
    [Header("POI Information")]
    [SerializeField] private string locationName;
    [SerializeField, TextArea(3,10)] private string description;
    
    [Header("GPS Coordinates")]
    [SerializeField, Range(-90f, 90f)] private float latitude;
    [SerializeField, Range(-180f, 180f)] private float longitude;
    
    // Public value-flag
    [Header("Status")]
    [SerializeField] public bool isVisited;
    
    // Access for Read-only (get)
    public string LocationName => locationName;
    public string Description => description;
    public float Latitude => latitude;
    public float Longitude => longitude;
    
    
    public Vector2 Coordinates => new Vector2(latitude, longitude);
}