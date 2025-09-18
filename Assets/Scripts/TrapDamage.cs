using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int dano = 1;
    private bool ativo = false;

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
    public void AtivarFogo()
    { 
        ativo = true; 
    }
    public void DesativarFogo()
    { 
        ativo = false;
    }
}
