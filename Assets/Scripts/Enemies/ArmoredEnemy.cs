public class ArmoredEnemy : Enemy
{
    void Awake()
    {
        _moveSpeed = 2f;
        _health = 50;
        EnemyPoints = 30;
    }
}
