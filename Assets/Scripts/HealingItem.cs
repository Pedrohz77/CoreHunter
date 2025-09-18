using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public int cura = 0;
    public KeyCode teclaUso = KeyCode.F;

    private bool jogadorPerto = false;
    private bool usado = false; // evita curar mais de uma vez
    private HeartSystem playerHearts;

    void Update()
    {
        if (jogadorPerto && !usado && Input.GetKeyDown(teclaUso))
        {
            if (playerHearts != null)
            {
                playerHearts.Curar(cura);
                usado = true; // marca como usado
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            playerHearts = other.GetComponent<HeartSystem>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            playerHearts = null;
        }
    }
}
