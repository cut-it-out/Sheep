using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 targetOffset;
    [SerializeField] float smoothSpeed = 3f;
    [SerializeField] float minZoom = 13f;
    [SerializeField] float maxZoom = 22f;
    [SerializeField] float zoomScale = .5f;

    public bool zoomDirectionInverted = false;

    private void Start()
    {
        transform.position = target.position + targetOffset;
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float mouseScrollDelta = zoomDirectionInverted ? -1 * Input.mouseScrollDelta.y : Input.mouseScrollDelta.y;
            float newYOffset = Mathf.Clamp(targetOffset.y - mouseScrollDelta * zoomScale, minZoom, maxZoom);
            targetOffset.y = newYOffset;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPos = target.position + targetOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;

    }

}
