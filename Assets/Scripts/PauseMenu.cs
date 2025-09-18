using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject vignetteOverlay; // ARRASTE AQUI SUA IMAGE DE VINHETA

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (vignetteOverlay != null)
            vignetteOverlay.SetActive(false); // esconde vinheta
        Time.timeScale = 1f; // volta o jogo
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (vignetteOverlay != null)
            vignetteOverlay.SetActive(true); // mostra vinheta
        Time.timeScale = 0f; // pausa o jogo
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // reset tempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recarrega cena atual
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu"); // troca pra cena de menu principal
    }
}
