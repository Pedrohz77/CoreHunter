using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;

    // Ajusta a direção que a bala vai seguir
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        // Faz a bala "girar" para a direção que está indo
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {
        // Movimento linear, sem gravidade
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // destrói quando sair da tela
    }
}
