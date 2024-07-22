using UnityEngine;

enum BonusType
{
    Weapon,
    PowerUp
}

public class Bonus : MonoBehaviour
{
    [SerializeField] BonusType _bonusType;

    public string BonusName;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerReceiver player))
        {
            if (_bonusType == BonusType.PowerUp)
                player.TakeBonusPowerUp(BonusName);
            else
                player.TakeBonusWeapon(BonusName);

            gameObject.SetActive(false);
            Destroy(gameObject, Random.Range(0f, 5f));
        }
    }
}
