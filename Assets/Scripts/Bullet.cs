using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletDamage { get; set; } = 1;

    void Start()
    {
        Invoke("InactivateBullet", 5f);
        Invoke("DestroyBullet", 5f);
    }

    void InactivateBullet() => gameObject.SetActive(false);

    void DestroyBullet() => Destroy(gameObject, Random.Range(0f, 3f));

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamageFromBullet(BulletDamage);
            gameObject.SetActive(false);
            DestroyBullet();
        }
        else
        {
            Invoke("InactivateBullet", 2f);
            Invoke("DestroyBullet", 3f);
        }

    }
}
