using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 playerDirection;
    private Rigidbody2D player;
    private Animator PlayerAnimator;

    //Movimentação e pulo
    private float walkspeed = 1f;
    private float currentSpeed;
    private bool walking;
    private float jumpForce = 4f;
    public bool FaceRight = true;

    private int jumpCount = 0;
    private int maxJumps = 2;

    // Combate
    private int punchCount = 0;
    private bool comboControl;
    private float timeCross = 1.5f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float shootCooldown = 0.2f; // Tempo mínimo entre disparos (em segundos)

    public GameObject punchHitbox; // arraste o hitbox do inspector

    private float lastShootTime = 0f; // Armazena o tempo do último disparo

    // DASH
    //public float dashForce = 10f;       // força do dash
    //public float dashTime = 0.2f;       // duração do dash
    //public float dashCooldown = 1f;     // recarga do dash
    private bool isDashing = false;
    //private bool canDash = true;

    private TrailRenderer trail; // <- referência para o trail

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>(); // pega o componente TrailRenderer
        if (trail != null) trail.enabled = false; // começa desativado
    }

    void Update()
    {
        if (!isDashing)
        {
            playerMove();
            Updateanimator();
            currentSpeed = walkspeed;

            // Attack
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Punch();
            }

            // Pulo duplo
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                player.linearVelocity = new Vector2(player.linearVelocity.x, 0f);
                player.linearVelocity = new Vector2(player.linearVelocity.x, jumpForce);
                jumpCount++;
            }

            // Correr
            if (Input.GetKey(KeyCode.LeftShift) && walking)
            {
                currentSpeed = walkspeed + 2f;
                PlayerAnimator.SetBool("Run", true);
            }
            else
            {
                currentSpeed = walkspeed;
                PlayerAnimator.SetBool("Run", false);
            }

            // Disparo
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time; // Atualiza o tempo do último disparo
        }
        }

        //// DASH → tecla CTRL
        //if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
        //{
        //    StartCoroutine(Dash());
        //}
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            float move = Input.GetAxisRaw("Horizontal");
            player.linearVelocity = new Vector2(move * currentSpeed, player.linearVelocity.y);
            walking = move != 0;
        }
    }

    void Updateanimator()
    {
        PlayerAnimator.SetBool("Walking", walking);
    }

    void playerMove()
    {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if (playerDirection.x > 0 && !FaceRight)
        {
            Flip();
        }
        else if (playerDirection.x < 0 && FaceRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        FaceRight = !FaceRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpCount = 0;
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }
    }

    // DASH Coroutine
    //IEnumerator Dash()
    //{
    //    canDash = false;
    //    isDashing = true;

    //    if (trail != null) trail.enabled = true; // ativa rastro

    //    float dashDirection = FaceRight ? 1f : -1f;
    //    player.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

    //    yield return new WaitForSeconds(dashTime);

    //    isDashing = false;

    //    if (trail != null) trail.enabled = false; // desativa rastro

    //    yield return new WaitForSeconds(dashCooldown);
    //    canDash = true;
    //}

    void Punch()
    {
        if (punchCount < 3)
        {
            punchCount++;
            PlayerAnimator.SetTrigger("Punch");

            if (!comboControl)
            {
                StartCoroutine(CrossController());
            }

            // Ativa o hitbox temporariamente
            StartCoroutine(EnablePunchHitbox());
        }
    }

    IEnumerator EnablePunchHitbox()
    {
        punchHitbox.SetActive(true);
        yield return new WaitForSeconds(0.2f); // duração do hitbox
        punchHitbox.SetActive(false);
    }
}


