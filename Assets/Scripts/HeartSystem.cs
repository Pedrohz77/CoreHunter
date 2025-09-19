using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartSystem : MonoBehaviour
{
    public int vida;
    public int vidaMaxima;

    public Image[] coracao;
    public Sprite cheio;
    public Sprite vazio;

    public Animator animator;

    void Start()
    {
        vida = vidaMaxima;
    }

    void Update()
    {
        HealthLogic();
    }

    void HealthLogic()
    {
        if (vida > vidaMaxima)
            vida = vidaMaxima;

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
                coracao[i].sprite = cheio;
            else
                coracao[i].sprite = vazio;

            coracao[i].enabled = i < vidaMaxima;
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;

        if (vida <= 0)
        {
            vida = 0;

            if (animator != null)
            {
                animator.Rebind();
                animator.Update(0f);
                animator.SetTrigger("DeadPlayer");
            }

            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;

            Invoke("LoadGameOverScene", 3f);
        }
    }

    public void Curar(int quantidade)
    {
        vida += quantidade;
        if (vida > vidaMaxima)
            vida = vidaMaxima;
    }

    void LoadGameOverScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
