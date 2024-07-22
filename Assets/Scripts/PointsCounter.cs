using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pointsCountText;
    [SerializeField] PlayerHealth _playerHealth;

    public int PlayerPoints { get; set; }

    void OnEnable() => _playerHealth.OnGameOver += SavePoints;

    void OnDisable() => _playerHealth.OnGameOver -= SavePoints;

    public void ShowPoints() => _pointsCountText.text = PlayerPoints.ToString() + " очков";

    void SavePoints()
    {
        if (PlayerPoints > PlayerPrefs.GetInt("Points"))
        {
            PlayerPrefs.SetInt("Points", PlayerPoints);
            PlayerPrefs.Save();
        }
    }
}
