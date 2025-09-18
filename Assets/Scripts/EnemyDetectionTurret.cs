using UnityEngine;

public class EnemyDetectionTurret : MonoBehaviour
{
    private EnemyTurret turret;

    void Start()
    {
        turret = GetComponentInParent<EnemyTurret>();

        // checa se o player já está dentro do trigger ao iniciar
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            turret.playerInRange = GetComponent<Collider2D>().bounds.Contains(playerObj.transform.position);
        }
    }

    void Update()
    {
        if (turret.player == null) return;

        turret.playerInRange = Vector2.Distance(transform.position, turret.player.position) <= turret.detectionRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            turret.playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            turret.playerInRange = false;
        }
    }
}