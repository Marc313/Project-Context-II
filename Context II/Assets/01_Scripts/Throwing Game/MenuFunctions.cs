using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Functions/Menu")]
public class MenuFunctions : ScriptableObject
{
    public int gameSceneIndex = 1;
    public int menuSceneIndex = 0;

    public void StartGame()
    {
        LoadScene(gameSceneIndex);
    }

    public void LoadMainMenu()
    {
        LoadScene(menuSceneIndex);
    }

    public void Quit()
    {
/*        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
        }*/
        Application.Quit();
    }

    private void LoadScene(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}
