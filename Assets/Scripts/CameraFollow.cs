using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _targetToFollow;
    [SerializeField] float _lerpRate;

    [SerializeField] Vector2 _length;
    [SerializeField] Vector2 _width;

    void Update()
    {
        Vector3 targetPosition = Vector3.Lerp(transform.position, _targetToFollow.position, Time.deltaTime * _lerpRate);

        float clampedX = Mathf.Clamp(targetPosition.x, _length.x, _length.y);
        float clampedZ = Mathf.Clamp(targetPosition.z, _width.x, _width.y);

        transform.position = new Vector3(clampedX, targetPosition.y, clampedZ);
    }
}
