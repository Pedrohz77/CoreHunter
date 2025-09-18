using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private bool playerNaArea = false;
    private EnergyCoreUI EnergyCoreManager;

    void Start()
    {
        EnergyCoreManager = Object.FindAnyObjectByType<EnergyCoreUI>();
    }

    void Update()
    {
        if (playerNaArea && Input.GetKeyDown(KeyCode.F))
        {
            Coletar();
        }
    }

    void Coletar()
    {
        Debug.Log("Núcleo coletado!");
        if (EnergyCoreManager != null)
        {
            EnergyCoreManager.ColetarNucleo();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaArea = false;
        }
    }
}
