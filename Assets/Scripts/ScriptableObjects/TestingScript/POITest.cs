using UnityEngine;

public class POITest : MonoBehaviour
{
    [Header("POI Asset for Testing")]
    public POIData testPOI;
    
    void Start()
    {
        TestPOI();
    }
    
    void TestPOI()
    {
        Debug.Log("=== TESTING ===\n");
        
        // 1. Reading data
        Debug.Log($"Location Name: {testPOI.LocationName}");
        Debug.Log($"Description: {testPOI.Description}");
        Debug.Log($"Coordinates: {testPOI.Coordinates}");
        Debug.Log($"Is vistited: {testPOI.isVisited}\n");
        
        // 2. Changing isVisited
        Debug.Log("Trying to change isVisited for true value...");
        testPOI.isVisited = true;
        Debug.Log($"Now isVisited = {testPOI.isVisited}\n");
        
        /* THE COMPILER SHOULD NOT ALLOW THE CODE TO RUN */
        // Uncomment it
        // 3. Trying to change read-only fileds
        
        // Debug.Log("trying to change locationName (read-only)...");
        // testPOI.locationName = "New LocationName"; // Should be ERROR in compilier
        // Debug.Log("This code doesn't compile - protection is working!\n");
        
        // 4. Trying to change Coordinates
        // Debug.Log("Trying to change coordinates directly..."); 
        // testPOI.Latitude = 45.0f;                              // Should be ERROR in compilier       
        // testPOI.Longitude = 50.0f;                             // Should be ERROR in compilier
        // Debug.Log($"New Coordinates: {testPOI.Coordinates}\n");
    }
}