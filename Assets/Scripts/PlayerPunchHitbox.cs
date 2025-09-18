using UnityEngine;

public class PlayerPunchHitbox : MonoBehaviour
{
    public int damage = 1; // quanto de dano o soco dá

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
