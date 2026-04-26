using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOpener : MonoBehaviour
{
    public string sceneToOpen;
    public void OpenScene()
    {
        SceneManager.LoadScene(sceneToOpen);
    }
}
