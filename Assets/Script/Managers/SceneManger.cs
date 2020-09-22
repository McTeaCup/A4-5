using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManger : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        GameplayScene,
        InfoScene
    }

    public void Gameplay()
    {
        SceneManager.LoadScene(Scene.GameplayScene.ToString());
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
