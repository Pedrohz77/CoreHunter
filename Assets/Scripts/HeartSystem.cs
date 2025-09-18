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
                // para todas as animações
                animator.Rebind();
                animator.Update(0f);

                // toca apenas a animação de morte
                animator.SetTrigger("DeadPlayer");
            }

            // trava movimento
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;

            // vai para GameOver após 3s
            Invoke("LoadGameOverScene", 3f);
        }
    }

    void LoadGameOverScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
