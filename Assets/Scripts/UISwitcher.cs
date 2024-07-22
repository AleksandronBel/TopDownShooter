using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PointsCounter _pointsCounter;

    [SerializeField] GameObject _gameOverWindow;
    [SerializeField] GameObject _inGamePoints;

    [SerializeField] TextMeshProUGUI _endPoints;

    void OnEnable()
    {
        _playerHealth.OnGameOver += SwitchCanvasWindows;
    }

    void OnDisable()
    {
        _playerHealth.OnGameOver -= SwitchCanvasWindows;
    }

    void SwitchCanvasWindows()
    {
        _gameOverWindow.SetActive(true);
        _inGamePoints.SetActive(false);
        SetPoints();
    }

    void SetPoints()
    {
        int dataFromPlayerPrefs = PlayerPrefs.GetInt("Points");

        if (_pointsCounter.PlayerPoints > dataFromPlayerPrefs)
            _endPoints.text = "Это новый рекорд!\n";

        _endPoints.text += "Ты заработал \n" + _pointsCounter.PlayerPoints.ToString() + " очков!";
    }
}
