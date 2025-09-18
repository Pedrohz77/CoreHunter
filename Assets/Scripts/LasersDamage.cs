using UnityEngine;

public class LasersDamage : MonoBehaviour
{
    public int dano = 1;               // quanto de dano o fogo causa
    private bool ativo = false;        // se está queimando no momento

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ativo && other.CompareTag("Player"))
        {
            HeartSystem vida = other.GetComponent<HeartSystem>();
            if (vida != null)
            {
                vida.TakeDamage(dano); // agora chama a função correta
            }
        }
    }

    // Chamado pelo Animation Event
    public void AtivarLasers()
    {
        ativo = true;
    }

    // Chamado pelo Animation Event
    public void DesativarLasers()
    {
        ativo = false;
    }
}
