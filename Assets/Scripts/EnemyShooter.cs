using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    // Player Detection
    public Transform playerTarget; 
    public float detectionRange = 5f;
    public float stoppingDistance = 3f;

    // Movimento
    public float moveSpeed = 2f;

    // Shoot
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireTimer;

    private bool playerDetected = false;

    private Animator animator;
    private Vector3 originalScale;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogWarning("Nenhum Animator encontrado no inimigo!");

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (playerTarget == null) return;

        float distance = Vector2.Distance(transform.position, playerTarget.position);

        // Detecta o player
        playerDetected = distance <= detectionRange;

        if (playerDetected)
        {
            if (distance > stoppingDistance)
            {
                MoveTowardsPlayer();
                animator.SetBool("EnemyRun", true);
            }
            else
            {
                animator.SetBool("EnemyRun", false);
                Shoot();
            }

            Flip();
        }
        else
        {
            animator.SetBool("EnemyRun", false);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Mira exatamente no TargetPoint
            Vector3 dir = (playerTarget.position - firePoint.position).normalized;

            bullet.GetComponent<BulletShooter>().SetDirection(dir);

            fireTimer = 0f;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;

        if (playerTarget.position.x > transform.position.x)
            scale.x = -Mathf.Abs(originalScale.x);   // direita
        else
            scale.x = Mathf.Abs(originalScale.x);    // esquerda

        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}
