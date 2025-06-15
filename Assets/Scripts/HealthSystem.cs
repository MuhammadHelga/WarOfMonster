using UnityEngine;
using UnityEngine.UI; // Tambahkan ini

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public int goldValueOnDeath = 5;
    public string baseTag = "";
    public static event System.Action OnPlayerBaseDestroyed;
    public static event System.Action OnEnemyBaseDestroyed;

    public event System.Action OnDeath;

    public GameObject goodOverPanel;
    public GameObject badOverPanel;

    // TAMBAHAN: Variabel untuk Health Bar UI
    public Slider healthBarSlider; // Jika menggunakan Slider
    // public Image healthBarImage; // Jika menggunakan Image

    void Start()
    {
        currentHealth = maxHealth;

        // TAMBAHAN: Inisialisasi Health Bar UI
        if (healthBarSlider != null) // Jika menggunakan Slider
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
        // else if (healthBarImage != null) // Jika menggunakan Image
        // {
        //     healthBarImage.fillAmount = currentHealth / maxHealth;
        // }


        if (goodOverPanel != null) goodOverPanel.SetActive(false);
        if (badOverPanel != null) badOverPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " hasar aldı. Mevcut canı: " + currentHealth);

        // TAMBAHAN: Perbarui Health Bar UI
        if (healthBarSlider != null) // Jika menggunakan Slider
        {
            healthBarSlider.value = currentHealth;
        }
        // else if (healthBarImage != null) // Jika menggunakan Image
        // {
        //     healthBarImage.fillAmount = currentHealth / maxHealth;
        // }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        OnDeath?.Invoke();
        string objectTag = gameObject.tag;

        if (!string.IsNullOrEmpty(baseTag) && objectTag == baseTag)
        {
            if (baseTag == "PlayerBase")
            {
                Debug.Log("Markas kamu hancur! Panel 'You Lost' muncul.");
                badOverPanel?.SetActive(true);
                Time.timeScale = 0f;
                OnPlayerBaseDestroyed?.Invoke();
            }
            else if (baseTag == "EnemyBase")
            {
                Debug.Log("Markas musuh hancur! Panel 'You Win' muncul.");
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
            Debug.LogWarning($"{gameObject.name} Tag tidak valid atau tidak terdefinisi untuk: '{objectTag}'");
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}