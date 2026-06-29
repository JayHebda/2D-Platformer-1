using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void RestartLevel()
    {
        string levelToLoad = PlayerPrefs.GetString("LastPlayedLevel", "Level 1");
        SceneManager.LoadScene(levelToLoad);
    }
}