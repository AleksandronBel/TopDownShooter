using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyHealth _enemyHealth;

    [SerializeField] protected int _health;
    [SerializeField] protected float _moveSpeed;

    public int EnemyPoints { get; set; }

    Transform _playerTransform;
    PointsCounter _playerPoints;

    void OnEnable() => _enemyHealth.OnHealthChanged += TakeDamage;

    void OnDisable() => _enemyHealth.OnHealthChanged -= TakeDamage;

    void Start()
    {
        _playerTransform = FindFirstObjectByType<PlayerReceiver>().transform;
        _playerPoints = FindFirstObjectByType<PointsCounter>();
    }

    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (_playerTransform != null)
        {
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, Random.Range(0f, 5f));

            _playerPoints.PlayerPoints += EnemyPoints;
            _playerPoints.ShowPoints();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerReceiver player))
        {
            player.CallDeath();
            gameObject.SetActive(false);
            Destroy(gameObject, Random.Range(0f, 5f));
        }
    }
}
