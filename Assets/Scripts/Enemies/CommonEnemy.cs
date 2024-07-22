public class CommonEnemy : Enemy
{
    void Awake()
    {
        _moveSpeed = 3f;
        _health = 10;
        EnemyPoints = 7;
    }
}
