public class FastEnemy : Enemy
{
    void Awake()
    {
        _moveSpeed = 4f;
        _health = 10;
        EnemyPoints = 12;
    }
}
