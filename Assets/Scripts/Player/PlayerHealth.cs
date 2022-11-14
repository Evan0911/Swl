using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats stats;

    public bool isInvicible = false;
    public float invincibilityFrameDelay = 0.2f;
    public float invincibilityTime = 3f;

    //public SpriteRenderer graphics;
    public Image graphics;

    public HealthBar healthBar;

    public static PlayerHealth instance;

    //public AudioClip hitSound;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        stats.currentHealth = stats.maxHealth;
        healthBar.SetMaxHealth(stats.maxHealth);
    }


    public void HealPlayer(int _amount)
    {
        if (stats.currentHealth + _amount >= 100)
        {
            stats.currentHealth = stats.maxHealth;
        }
        else
        {
            stats.currentHealth += _amount;
        }
        healthBar.SetHealth(stats.currentHealth);
    }

    public void TakeDamage(int _damage)
    {
        //Quand le joueur n'est pas invincible il peut prendre des dégats
        if (!isInvicible)
        {
            //AudioManager.Instance.PlayClipAt(hitSound, transform.position);
            if (_damage - PlayerStats.instance.defFlat > 0)
            {
                _damage -= PlayerStats.instance.defFlat;
                if (PlayerStats.instance.isBlocking)
                {
                    _damage = _damage / 2;
                }
                stats.currentHealth -= _damage;
                healthBar.SetHealth(stats.currentHealth);
                isInvicible = true;
                //Si le joueur est mort
                if (stats.currentHealth <= 0)
                {
                    BattleManager.instance.Lose();
                    return;
                }
                else
                {
                    //Les coroutines gèrent le temps d'invincibilité
                    StartCoroutine(InvincibilityFrameGraphic());
                    StartCoroutine(HandleInvincibilityDelay());
                }
            }
        }
    }

    //Coroutine 1, fais clignoter le joueur
    public IEnumerator InvincibilityFrameGraphic()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFrameDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFrameDelay);
        }
    }

    //Coroutine 2, rend le joueur vulnérable au bout de "InvincibilityTime" secondes
    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvicible = false;
    }
    /*
    public void Death()
    {
        PlayerMovement.instance.RB.velocity = Vector3.zero;
        PlayerMovement.instance.animator.SetTrigger("Death");
        PlayerMovement.instance.enabled = false;
        PlayerMovement.instance.RB.bodyType = RigidbodyType2D.Kinematic;
        PlayerMovement.instance.playerCollider.enabled = false;
        GameOverManager.instance.OnPlayerDeath();
    }

    public void Respawn()
    {
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.RB.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }*/
}
