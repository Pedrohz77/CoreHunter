using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static GameOverMenu instance;
    public string gameOverSceneName = "GameOver"; // cena de Game Over
    public string firstFaseName = "Fase1"; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔴 Chame este método quando o player morrer
    public void TriggerGameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }

    // Botão Restart -> sempre volta pra primeira fase
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(firstFaseName);
    }

    // Botão de voltar pro menu inicial
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu"); // nome da cena do menu inicial
    }
}
