using UnityEngine;

public class BulletTurret : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    public int dano = 1; // quanto dano o tiro vai causar

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se colidiu com o player
        HeartSystem playerHealth = collision.GetComponent<HeartSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(dano); // aplica o dano
            Destroy(gameObject); // destrói o tiro ao atingir o player
        }

    }
}
