using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] float _bulletSpeed;

    [SerializeField] LayerMask _enemyLayerMask;

    [SerializeField] PlayerReceiver _playerReceiver;

    Coroutine _currentCoroutine;

    float _fireRate = 1f;
    float _nextFireTime;
    float _explosionRadiusGrenadeLauncher = 2f;
    int _bulletDamage = 1;

    string _currentWeapon;

    void OnEnable() => _playerReceiver.OnWeaponBonusChanged += ApplyWeaponStats;

    void OnDisable() => _playerReceiver.OnWeaponBonusChanged -= ApplyWeaponStats;

    void Start()
    {
        SetStartWeapon();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
    }

    void SetStartWeapon()
    {
        _bulletDamage = 1;
        _bulletSpeed = 30f;
        _fireRate = 1f;
    }

    void ApplyWeaponStats(string weapon)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentWeapon = weapon;

        switch (_currentWeapon)
        {
            case "Pistol":
                _bulletDamage = 3;
                _fireRate = 2f;
                _bulletSpeed = 20f;
                break;
            case "Rifle":
                _bulletDamage = 1;
                _fireRate = 10f;
                _bulletSpeed = 25f;
                break;
            case "Shotgun":
                _bulletDamage = 2;
                _fireRate = 1.5f;
                _bulletSpeed = 15f;
                break;
            case "GrenadeLauncher":
                _bulletDamage = 10;
                _fireRate = 0.66f;
                _bulletSpeed = 10f;
                break;
        }
        _currentCoroutine = StartCoroutine(RemoveWeaponBoost());
    }

    IEnumerator RemoveWeaponBoost()
    {
        yield return new WaitForSeconds(10f);
        _currentWeapon = null;
        SetStartWeapon();
    }

    void Shoot()
    {
        if (_currentWeapon == "Shotgun")
            ShootShotgun();
        else if (_currentWeapon == "GrenadeLauncher")
            ShootGrenade();
        else
            ShootSingleBullet();
    }

    void ShootShotgun()
    {
        int shotgunsBlast = 5;
        float shotAngle = 10f;

        for (int i = 0; i < shotgunsBlast; i++)
        {
            float angle = shotAngle * (i - (shotgunsBlast - 1) / 2f);
            Quaternion rotation = Quaternion.AngleAxis(angle, _bulletSpawnPoint.up);
            Vector3 direction = rotation * _bulletSpawnPoint.forward;

            GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.LookRotation(direction));
            newBullet.GetComponent<Bullet>().BulletDamage = _bulletDamage;
            newBullet.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
            StartCoroutine(LimitBulletRange(newBullet, 7f));
        }
    }

    void ShootGrenade()
    {
        Vector3 targetPoint = GetMouseWorldPosition();

        GameObject newGrenade = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        newGrenade.GetComponent<Bullet>().BulletDamage = _bulletDamage;
        newGrenade.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(MoveGrenadeToPoint(newGrenade, targetPoint));
    }

    void ShootSingleBullet()
    {
        GameObject newBullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        newBullet.GetComponent<Bullet>().BulletDamage = _bulletDamage;
        newBullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.forward * _bulletSpeed;
    }

    IEnumerator LimitBulletRange(GameObject bullet, float maxRange)
    {
        float traveledDistance = 0f;
        Vector3 lastPosition = bullet.transform.position;

        while (traveledDistance < maxRange && bullet.activeSelf)
        {
            traveledDistance += Vector3.Distance(lastPosition, bullet.transform.position);
            lastPosition = bullet.transform.position;
            yield return null;
        }

        if (bullet != null)
        {
            bullet.SetActive(false);
            Destroy(bullet, Random.Range(0f, 3f));
        }
    }

    IEnumerator MoveGrenadeToPoint(GameObject grenade, Vector3 targetPoint)
    {
        Vector3 startPosition = grenade.transform.position;
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime;
            grenade.transform.position = Vector3.Lerp(startPosition, targetPoint, time);
            yield return null;
        }

        Explode(grenade, targetPoint);
    }

    void Explode(GameObject grenade, Vector3 explosionPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, _explosionRadiusGrenadeLauncher, _enemyLayerMask);

        foreach (var collider in colliders)
            collider.GetComponent<EnemyHealth>()?.TakeDamageFromBullet(_bulletDamage);

        grenade.SetActive(false);
        Destroy(grenade, Random.Range(0f, 3f));
    }

    public string GetCurrentWeapon()
    {
        return _currentWeapon;
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;

        return Vector3.zero;
    }
}