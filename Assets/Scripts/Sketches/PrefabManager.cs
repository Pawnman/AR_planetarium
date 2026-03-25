using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField]
    private List<PrefabEntry> prefabEntries = new List<PrefabEntry>();

    // Dictionary for fast access
    private Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    // Singletone for global access
    public static PrefabManager Instance { get; private set; }

    private void Awake()
    {
        // Singletone
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildDictionary();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// Creating dictionary with ID and prefabs check

    private void BuildDictionary()
    {
        prefabDictionary.Clear();

        foreach (var entry in prefabEntries)
        {
            if (string.IsNullOrEmpty(entry.id))
            {
                Debug.LogWarning($"Prefab with empty ID was skipped!");
                continue;
            }

            if (entry.prefab == null)
            {
                Debug.LogWarning($"Prefab for ID '{entry.id}' doesn't exist!");
                continue;
            }

            if (prefabDictionary.ContainsKey(entry.id))
            {
                Debug.LogWarning($"Duplicate ID '{entry.id}'! The latest one will be used.");
            }

            prefabDictionary[entry.id] = entry.prefab;
        }

        Debug.Log($"PrefabLibrary initialized. Uploaded {prefabDictionary.Count} prefabs.");
    }
    // Receiving prefab (reading) from ID
    public GameObject GetPrefab(string id)
    {
        if (prefabDictionary.TryGetValue(id, out GameObject prefab))
        {
            return prefab;
        }

        Debug.LogError($"Prefab '{id}' wasn't found");
        return null;
    }

    [System.Serializable]
    public class PrefabEntry
    {
        [Tooltip("ID")]
        public string id;

        [Tooltip("Prefab link")]
        public GameObject prefab;
    }
}