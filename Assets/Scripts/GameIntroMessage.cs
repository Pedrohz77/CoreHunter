using UnityEngine;
using TMPro; // necessário para TextMeshPro

public class GameIntroMessage : MonoBehaviour
{
    public TextMeshProUGUI objetivoTexto; // arraste o texto do Canvas aqui no Inspector
    public float tempoExibicao = 5f;

    void Start()
    {
        StartCoroutine(ShowObjective());
    }

    private System.Collections.IEnumerator ShowObjective()
    {
        // Ativa o texto
        objetivoTexto.gameObject.SetActive(true);

        // Espera os segundos definidos
        yield return new WaitForSeconds(tempoExibicao);

        // Desativa o texto
        objetivoTexto.gameObject.SetActive(false);
    }
}
