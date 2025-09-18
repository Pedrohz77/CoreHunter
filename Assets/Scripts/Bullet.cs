using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletTime = 2f;        // tempo de vida da bala
    public float maxDistance = 4f;       // distância máxima
    public int damage = 1;               // dano da bala
    private Vector3 startPosition;       // posição inicial da bala

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, BulletTime); // destrói a bala após X segundos
    }

    void Update()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto colidido tem um componente EnemyHealth
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // aplica dano
        }

        Destroy(gameObject); // destrói a bala ao colidir
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPosition, startPosition + transform.right * maxDistance);
            Gizmos.DrawWireSphere(startPosition + transform.right * maxDistance, 0.2f);
        }
    }
}
