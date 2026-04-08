using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Navigation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer mapSprite;

    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.05f;
    
    [Header("Pan & Zoom Settings")]
    [SerializeField] private float panSensitivity = 2.0f;
    [SerializeField] private float zoomCooldown = 0.1f;
    [SerializeField] private float zoomMin = 15f;
    [SerializeField] private float zoomMax = 60f;
    [SerializeField] private float mobileZoomSensitivity = 2.0f;
    [SerializeField] private float mouseScrollSensitivity = 20.0f;

    private Camera _cam;
    private Vector3 _targetPos;
    private float _targetZoom;
    
    private Vector3 _dragStartWorldPos;
    private Vector3 _camStartPosAtDrag;
    private Vector3 _moveVel;
    private float _zoomVel;
    
    private float _lastZoomTime;
    private bool _isPanningAllowed;

    private void Awake()
    {
        // Camera Initialization
        _cam = GetComponent<Camera>();
        _targetPos = transform.position;
        _targetZoom = _cam.orthographicSize;
    }

    private void LateUpdate()
    {
        HandleInputs();
        ApplyMotion();
    }

    private void HandleInputs()
    {
        // ZOOM LOGIC
        if (Input.touchCount >= 2)
        {
            ProcessTouchZoom();
            return; 
        }

        ProcessMouseZoom();

        // PAN (MOVE) LOGIC
        if (Time.time - _lastZoomTime > zoomCooldown)
        {
            ProcessPan();
        }
    }

    private void ProcessTouchZoom()
    {
        if (UIUtils.IsPointerOverUI())
        {
            _isPanningAllowed = false;
            return;
        }

        float delta = MapInput.GetPinchDelta();
        if (Mathf.Abs(delta) < 0.1f) return;

       
        float modifier = (delta / Screen.height) * _targetZoom * mobileZoomSensitivity;
        _targetZoom = Mathf.Clamp(_targetZoom - modifier, zoomMin, zoomMax);
        
        _lastZoomTime = Time.time;
        _isPanningAllowed = true; 
    }

    private void ProcessMouseZoom()
    {
        float scroll = MapInput.GetMouseScroll();
        if (scroll == 0) return;

        _targetZoom = Mathf.Clamp(_targetZoom - scroll * mouseScrollSensitivity, zoomMin, zoomMax);
    }

    private void ProcessPan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPanningAllowed = !UIUtils.IsPointerOverUI();
            if (_isPanningAllowed)
            {
                _dragStartWorldPos = GetWorldPos(Input.mousePosition);
                _camStartPosAtDrag = _targetPos;
            }
        }

        if (Input.GetMouseButton(0) && _isPanningAllowed)
        {
            Vector3 currentWorldPos = GetWorldPos(Input.mousePosition);
            _targetPos = _camStartPosAtDrag + (_dragStartWorldPos - currentWorldPos) * panSensitivity;
        }

        if (Input.GetMouseButtonUp(0)) _isPanningAllowed = false;
    }

    private void ApplyMotion()
    {
        _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _targetZoom, ref _zoomVel, smoothTime);

        if (mapSprite != null)
        {
            _targetPos = ClampToMap(_targetPos);
        }

        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref _moveVel, smoothTime);
    }

    private Vector3 ClampToMap(Vector3 pos)
    {
        float h = _cam.orthographicSize;
        float w = h * _cam.aspect;
        Bounds b = mapSprite.bounds;

        float minX = b.min.x + w; float maxX = b.max.x - w;
        float minY = b.min.y + h; float maxY = b.max.y - h;

        if (maxX < minX) minX = maxX = b.center.x;
        if (maxY < minY) minY = maxY = b.center.y;

        return new Vector3(Mathf.Clamp(pos.x, minX, maxX), Mathf.Clamp(pos.y, minY, maxY), transform.position.z);
    }

    private Vector3 GetWorldPos(Vector3 screenPos)
    {
        screenPos.z = Mathf.Abs(transform.position.z);
        return _cam.ScreenToWorldPoint(screenPos);
    }
}

