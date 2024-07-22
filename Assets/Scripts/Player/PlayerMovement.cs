using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _baseMoveSpeed = 4f;
    [SerializeField] float _slowZoneMultiplier = 0.6f;
    [SerializeField] float _speedBonusMultiplier = 1.5f;
    [SerializeField] float _currentSpeedMultiplier = 1f;

    [SerializeField] float _rotationSpeed = 180f;

    [SerializeField] Camera _playerCamera;

    [SerializeField] Vector2 _length;
    [SerializeField] Vector2 _width;

    [SerializeField] PlayerReceiver _playerReceiver;

    Vector3 _mousePosition;

    void OnEnable()
    {
        _playerReceiver.OnSpeedDecreaseZoneChanged += ChangeSpeedFromZone;
        _playerReceiver.OnSpeedIncreaseBonusChanged += ChangeSpeedFromBonus;
    }

    void OnDisable()
    {
        _playerReceiver.OnSpeedDecreaseZoneChanged -= ChangeSpeedFromZone;
        _playerReceiver.OnSpeedIncreaseBonusChanged -= ChangeSpeedFromBonus;
    }

    void Update()
    {
        Move();
        RotatePlayerToMouse();
    }

    void RotatePlayerToMouse()
    {
        Vector3 direction = GetMouseWorldPosition() - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float currentSpeed = _baseMoveSpeed * _currentSpeedMultiplier;
        Vector3 offset = new Vector3(h, 0, v) * currentSpeed * Time.deltaTime;
        transform.Translate(offset);

        float clampedX = Mathf.Clamp(transform.position.x, _length.x, _length.y);
        float clampedZ = Mathf.Clamp(transform.position.z, _width.x, _width.y);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    void ChangeSpeedFromZone(bool isSpeedChanged) => _currentSpeedMultiplier = isSpeedChanged ? _slowZoneMultiplier : 1f;

    void ChangeSpeedFromBonus(bool isSpeedChanged) => _currentSpeedMultiplier = isSpeedChanged ? _speedBonusMultiplier : 1f;
}
