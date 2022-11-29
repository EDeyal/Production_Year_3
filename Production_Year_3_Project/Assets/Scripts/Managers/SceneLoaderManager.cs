using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadNextScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadAdditiveScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
