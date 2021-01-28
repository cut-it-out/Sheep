using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform lookAtTarget;


    [SerializeField] Vector3 targetOffset;
    [SerializeField] Vector3 lookAtOffset;

    [SerializeField] float smoothSpeed = 2f;

    private void LateUpdate()
    {
        Vector3 desiredPos = target.position + targetOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;

        transform.LookAt(lookAtTarget.position + lookAtOffset);

    }
}
