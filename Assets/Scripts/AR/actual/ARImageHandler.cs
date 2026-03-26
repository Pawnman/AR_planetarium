using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARImageHandler : MonoBehaviour
{
    [System.Serializable]
    public struct MarkerPrefab
    {
        public string markerName;
        public GameObject prefab;
    }

    [Header("Setting for markers")]
    [SerializeField] private List<MarkerPrefab> markerPrefabPairs = new List<MarkerPrefab>();

    private ARTrackedImageManager _trackedImageManager;
    private Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackablesChanged.AddListener(OnChanged);
    }

    void OnDisable()
    {
        if (_trackedImageManager != null)
            _trackedImageManager.trackablesChanged.RemoveListener(OnChanged);
    }

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            UpdateImage(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            UpdateImage(updatedImage);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;

        if (!_instantiatedPrefabs.ContainsKey(name))
        {
            foreach (var pair in markerPrefabPairs)
            {
                if (pair.markerName == name)
                {
                    var instance = Instantiate(pair.prefab, trackedImage.transform);
                    _instantiatedPrefabs.Add(name, instance);
                    break;
                }
            }
        }

        if (_instantiatedPrefabs.TryGetValue(name, out GameObject prefabInstance))
        {
            // Settting up activity for object - depends on tracking status 
            bool isTracking = trackedImage.trackingState == TrackingState.Tracking;
            prefabInstance.SetActive(isTracking);
        }
    }
}