using UnityEngine;
using UnityEngine.SceneManagement;
// KI-Generiert


public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        //Application.Quit();
        Debug.Log("Spiel wird beendet");
    }
    
}