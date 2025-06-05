using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public int goldValueOnDeath = 5;
    public string baseTag = ""; // Inspector'dan "PlayerBase" veya "EnemyBase" atanmalı
    public static event System.Action OnPlayerBaseDestroyed;
    public static event System.Action OnEnemyBaseDestroyed;

    public event System.Action OnDeath;

    public GameObject goodOverPanel;
    public GameObject badOverPanel;

    void Start()
    {
        currentHealth = maxHealth;

        if (goodOverPanel != null) goodOverPanel.SetActive(false);
        if (badOverPanel != null) badOverPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " hasar aldı. Mevcut canı: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();

        // Tag güvenli şekilde kontrol edilir
        string objectTag = gameObject.tag;

        if (!string.IsNullOrEmpty(baseTag) && objectTag == baseTag)
        {
            if (baseTag == "PlayerBase")
            {
                Debug.Log("Oyuncu base'i yıkıldı! Kaybettin Paneli Açılıyor.");
                badOverPanel?.SetActive(true);
                Time.timeScale = 0f;
                OnPlayerBaseDestroyed?.Invoke();
            }
            else if (baseTag == "EnemyBase")
            {
                Debug.Log("Düşman base'i yıkıldı! Zafer Paneli Açılıyor.");
                goodOverPanel?.SetActive(true);
                Time.timeScale = 0f;
                OnEnemyBaseDestroyed?.Invoke();
            }

            Destroy(gameObject);
        }
        else if (objectTag == "PlayerUnit")
        {
            EnemyCoinController.instance?.GainCoin(goldValueOnDeath);
            Destroy(gameObject);
        }
        else if (objectTag == "EnemyUnit")
        {
            PlayerCoinController.instance?.GainCoin(goldValueOnDeath);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} için geçersiz veya tanımsız tag: '{objectTag}'");
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
