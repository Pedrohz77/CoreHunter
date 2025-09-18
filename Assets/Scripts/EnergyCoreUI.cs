using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnergyCoreUI : MonoBehaviour
{
    public Image[] nucleos; // arraste as imagens no inspetor
    public TextMeshProUGUI mensagemFinal;
    public float delayParaVitoria = 3f; // tempo antes de ir para a cena de vit�ria

    private int nucleosColetados = 0;

    public void ColetarNucleo()
    {
        if (nucleosColetados < nucleos.Length)
        {
            nucleos[nucleosColetados].enabled = false;
            nucleosColetados++;
        }

        if (nucleosColetados == nucleos.Length)
        {
            if (mensagemFinal != null)
            {
                mensagemFinal.gameObject.SetActive(true);
                mensagemFinal.text = "Todos os n�cleos coletados!";
            }

            // inicia a contagem para ir � cena de vit�ria
            Invoke(nameof(CarregarCenaVitoria), delayParaVitoria);
        }
    }

    private void CarregarCenaVitoria()
    {
        SceneManager.LoadScene("VictoryScene"); // coloque aqui o nome da sua cena de vit�ria
    }
}
