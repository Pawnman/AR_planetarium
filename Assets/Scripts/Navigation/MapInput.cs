using UnityEngine;

public static class MapInput
{
    private const float SCROLL_THRESHOLD = 0.001f;

    public static float GetPinchDelta()
    {
        if (Input.touchCount < 2) return 0f;
        
        Touch t0 = Input.GetTouch(0);
        Touch t1 = Input.GetTouch(1);
        
        Vector2 t0Prev = t0.position - t0.deltaPosition;
        Vector2 t1Prev = t1.position - t1.deltaPosition;
        
        float prevMag = (t0Prev - t1Prev).magnitude;
        float currMag = (t0.position - t1.position).magnitude;
        
        return currMag - prevMag;
    }

    public static float GetMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > SCROLL_THRESHOLD)
        {
            return scroll;
        }
        else
        {
            return 0f;
        }
    }
}
