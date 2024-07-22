using System;
using System.Collections;
using UnityEngine;

public class PlayerReceiver : MonoBehaviour
{
    public Action<bool> OnSpeedDecreaseZoneChanged;

    public Action<bool> OnSpeedIncreaseBonusChanged;
    public Action<bool> OnInvulnerabilityBonusChanged;

    public Action<string> OnWeaponBonusChanged;

    public Action OnDeathChanged;

    public void DecreaseMovementSpeedFromZone(bool isSpeedChanged) => OnSpeedDecreaseZoneChanged?.Invoke(isSpeedChanged);

    public void ChangeMovementSpeedFromBonus(bool isSpeedChanged) => OnSpeedIncreaseBonusChanged?.Invoke(isSpeedChanged);

    public void ChangeInvulnerabilityFromBonus(bool isInvulnerabilityChanged) => OnInvulnerabilityBonusChanged?.Invoke(isInvulnerabilityChanged);

    public void CallDeath() => OnDeathChanged?.Invoke();
    
    public void TakeBonusWeapon(string weaponName) => OnWeaponBonusChanged?.Invoke(weaponName);

    public void TakeBonusPowerUp(string powerUpName) => ApplyPowerUpEffect(powerUpName);
    
    void ApplyPowerUpEffect(string powerUpName)
    {
        switch (powerUpName)
        {
            case "Speed":
                ChangeMovementSpeedFromBonus(true);
                StartCoroutine(RemoveSpeedBoost());
                break;
            case "Invulnerability":
                ChangeInvulnerabilityFromBonus(true);
                StartCoroutine(RemoveInvulnerability());
                break;
        }
    }

    IEnumerator RemoveSpeedBoost()
    {
        yield return new WaitForSeconds(10f);
        ChangeMovementSpeedFromBonus(false);
    }

    IEnumerator RemoveInvulnerability()
    {
        yield return new WaitForSeconds(10f);
        ChangeInvulnerabilityFromBonus(false);
    }
}
