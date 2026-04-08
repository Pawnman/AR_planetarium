using UnityEngine;
using UnityEngine.EventSystems;

// Check touches above the UI

public static class UIUtils
{
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        
        // Unity Editor
        if (EventSystem.current.IsPointerOverGameObject()) return true;

        // Mobile
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
                return true;
        }
        return false;
    }
}
