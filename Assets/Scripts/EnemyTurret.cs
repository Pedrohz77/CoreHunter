using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 1.5f;
    private float shootTimer;

    private Animator animator;

    public Transform player;
    private bool facingRight = true;

    [HideInInspector] public bool playerInRange;

    [Header("Zona de Detecção")]
    public float detectionRadius = 5f; // raio da área de visão



    void Start()
    {
        animator = GetComponent<Animator>();
        shootTimer = shootCooldown;
    }

    void Update()
    {
        if (!playerInRange) return; // só atira se player estiver dentro da zona

        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }

        HandleFlip();
    }

    void Shoot()
    {
        if (!playerInRange) return; // só atira se o player estiver na zona
        animator.SetTrigger("ShootEnemy");
        Debug.Log("Shoot chamado!");
    }

    // Chamado pelo evento da animação
    public void FireBullet()
    {
        if (bulletPrefab == null || player == null) return;

        // direção até o player
        Vector2 direction = (player.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // rotaciona a bala para mirar no player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        // envia direção para a bala
        bullet.GetComponent<BulletTurret>().SetDirection(direction);
    }


    void HandleFlip()
    {
        if (player == null) return;

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }

        Debug.Log("Player entrou!");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // --- DESENHO DO GIZMO ---
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
