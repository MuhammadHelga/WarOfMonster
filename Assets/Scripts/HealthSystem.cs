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
        // Debug.Log($"{gameObject.name} menerima damage: {amount}");
        Debug.Log($"[DEBUG] {gameObject.name} terkena damage");

        currentHealth -= amount;
        Debug.Log(gameObject.name + " diserang. Darah saat ini : " + currentHealth);

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

    protected void Die()
{
    OnDeath?.Invoke();
    string objectTag = gameObject.tag;
    Debug.Log($"[DIE] Object: {gameObject.name}, Tag: {objectTag}, baseTag: {baseTag}");

    if (!string.IsNullOrEmpty(baseTag) && objectTag == baseTag)
    {
        Debug.Log("Base logic dipanggil!");

        if (baseTag == "PlayerBase")
        {
            Debug.Log("Markas kamu hancur!");
            badOverPanel?.SetActive(true);
            Time.timeScale = 0f;
            OnPlayerBaseDestroyed?.Invoke();
        }
        else if (baseTag == "EnemyBase")
        {
            Debug.Log("Markas musuh hancur!");
            goodOverPanel?.SetActive(true);
            Time.timeScale = 0f;
            OnEnemyBaseDestroyed?.Invoke();
        }

        Destroy(gameObject);
    }
    else if (objectTag == "PlayerUnit")
    {
        Debug.Log("Unit player mati");
        EnemyCoinController.instance?.GainCoin(goldValueOnDeath);
        Destroy(gameObject);
    }
    else if (objectTag == "EnemyUnit")
    {
        Debug.Log("Unit musuh mati");
        PlayerCoinController.instance?.GainCoin(goldValueOnDeath);
        Destroy(gameObject);
    }
    else
    {
        Debug.LogWarning($"[DIE] Tag tidak cocok atau tidak diketahui: {objectTag}");
    }
}


    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}