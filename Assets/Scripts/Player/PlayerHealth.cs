using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int health;

    public TMP_Text healthText;
    private PlayerDamageEffect damageEffects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthText.text = health.ToString();
        damageEffects = GetComponent<PlayerDamageEffect>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        UpdateHealthText();

        damageEffects.PlayDamageEffects();

        if (health <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }

    void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
}
