using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassagemFase : MonoBehaviour
{
    public string nextSceneName;      // Nome da próxima cena
    public Animator doorAnimator;     // Animator da porta (opcional)
    public GameObject pressFUI;       // UI que aparece tipo "Pressione F"

    private bool playerNearby = false;

    void Start()
    {
        if (pressFUI != null)
            pressFUI.SetActive(false); // garante que a UI começa invisível
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // Toca animação de abrir antes de trocar de cena
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("OpenPorta");
                // se quiser esperar animação antes de trocar, pode usar coroutine
                Invoke("LoadNextScene", 3f); // espera 0.5s antes de carregar
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (pressFUI != null)
                pressFUI.SetActive(true); // mostra UI
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (pressFUI != null)
                pressFUI.SetActive(false); // esconde UI
        }
    }
}
