using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void LoadSceneIndex(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadAdditiveScene(int sceneNumber)
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
    public Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public void ResetActiveScene()
    {
        LoadSceneIndex(GetActiveScene().buildIndex);
    }
}
