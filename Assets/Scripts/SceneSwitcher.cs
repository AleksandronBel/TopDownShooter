using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
