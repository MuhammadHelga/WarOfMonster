using UnityEngine;
using UnityEngine.UI; // Hala UI namespace'ine ihtiyacınız olabilir (örneğin Button için)
using TMPro;       // TextMeshPro namespace'ini ekledik

public class PlayerCoinController : MonoBehaviour
{
    public int playerCoin = 100; // Başlangıç oyuncu altını
    public TextMeshProUGUI coinText; // Canvas üzerindeki altın miktarını gösteren TextMeshPro bileşeni

    public static PlayerCoinController instance; // Singleton pattern

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateCoinText();
    }

    public bool CanAfford(int amount)
    {
        return playerCoin >= amount;
    }

    public void SpendCoin(int amount)
    {
        if (CanAfford(amount))
        {
            playerCoin -= amount;
            UpdateCoinText();
        }
        else
        {
            Debug.Log("Yeterli altın yok!");
        }
    }

    public void GainCoin(int amount)
    {
        playerCoin += amount;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Altın: " + playerCoin;
        }
        else
        {
            Debug.LogError("Player Coin TextMeshPro bileşeni atanmamış!");
        }
    }
}