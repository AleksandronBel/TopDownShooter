using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerReceiver _playerReceiver;
    [SerializeField] PointsCounter _pointsCounter;

    public Action OnGameOver;

    bool _isReadyToReceiveDamage;

    void Start()
    {
        _isReadyToReceiveDamage = true;
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        _playerReceiver.OnInvulnerabilityBonusChanged += ChangeInvulnerabilityFromBonus;
        _playerReceiver.OnDeathChanged += DeathPlayer;
    }

    void OnDisable()
    {
        _playerReceiver.OnInvulnerabilityBonusChanged -= ChangeInvulnerabilityFromBonus;
        _playerReceiver.OnDeathChanged -= DeathPlayer;
    }

    void ChangeInvulnerabilityFromBonus(bool isInvulnerabilityChanged)
    {
        if (isInvulnerabilityChanged)
            _isReadyToReceiveDamage = false;
        else
            _isReadyToReceiveDamage = true;
    }

    void DeathPlayer()
    {
        if (_isReadyToReceiveDamage) 
        {
            OnGameOver?.Invoke();
            Time.timeScale = 0f;
        }
    }
}
