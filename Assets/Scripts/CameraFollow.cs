using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    [SerializeField] private Collider2D levelBounds;

    private Camera cam;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (levelBounds != null)
        {
            minBounds = levelBounds.bounds.min;
            maxBounds = levelBounds.bounds.max;
        }
    }
    void LateUpdate()
    {
        if (player == null) return;
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        if (levelBounds != null)
        {
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x + camWidth, maxBounds.x - camWidth);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHeight, maxBounds.y - camHeight);
        }

        transform.position = smoothedPosition;
    }

}

