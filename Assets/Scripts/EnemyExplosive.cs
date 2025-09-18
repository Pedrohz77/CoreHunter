using UnityEngine;

public class EnemyExplosive : MonoBehaviour
{
    public Transform player;             // Referência ao player
    public float speed = 2f;             // Velocidade do inimigo
    public float explodeDistance = 1f;   // Distância para começar a explosão
    public int damage = 1;               // Quanto de dano vai causar

    private Animator animator;
    private bool isExploding = false;    // Para não repetir explosão
    private bool playerDetected = false; // Só persegue se detectar o player

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isExploding || !playerDetected) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > explodeDistance)
        {
            // Move em direção ao player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Liga animação de andar
            animator.SetBool("EnemyWalking", true);
        }
        else
        {
            // Ativa explosão
            animator.SetBool("EnemyWalking", false);
            animator.SetTrigger("Explode");
            isExploding = true;

            // Aplica dano no player (se tiver script de vida)
            HeartSystem playerHealth = player.GetComponent<HeartSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Duração da animação antes de destruir o inimigo
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = true; // Player entrou na zona de detecção
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerDetected = false; // Player saiu da zona, inimigo para
            animator.SetBool("EnemyWalking", false);
        }
    }
}
