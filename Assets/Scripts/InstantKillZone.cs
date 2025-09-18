using UnityEngine;

public class InstantKillZone : MonoBehaviour
{
    public int dano = 3; // valor alto pra garantir hit kill (ajuste se precisar)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HeartSystem vida = other.GetComponent<HeartSystem>();
            if (vida != null)
            {
                vida.TakeDamage(dano); // causa dano direto (3)
            }
        }
    }
}
