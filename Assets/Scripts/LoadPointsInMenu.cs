using TMPro;
using UnityEngine;

public class LoadPointsInMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pointsCountText;

    void Start()
    {
        _pointsCountText.text = PlayerPrefs.GetInt("Points").ToString() + " очков!";
    }
}
