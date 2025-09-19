using UnityEngine;

public class ItemCura : MonoBehaviour
{
    public int quantidadeCura = 1; // cura apenas 1 coração
    private bool playerNaArea = false;
    private HeartSystem heartSystem;

    void Update()
    {
        if (playerNaArea && Input.GetKeyDown(KeyCode.F))
        {
            if (heartSystem != null)
            {
                heartSystem.Curar(quantidadeCura);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            heartSystem = other.GetComponent<HeartSystem>();
            playerNaArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNaArea = false;
            heartSystem = null;
        }
    }
}
