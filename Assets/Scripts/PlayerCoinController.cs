using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerCoinController : MonoBehaviour
{
    public int playerCoin = 100;
    public TextMeshProUGUI coinText;

    public static PlayerCoinController instance;

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
        StartCoroutine(GainCoinEverySecond());
    }

    private IEnumerator GainCoinEverySecond()
    {
        while (true)
        {
            GainCoin(3);
            yield return new WaitForSeconds(5f);
        }
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
            Debug.Log("Gold tidak mencukupi!");
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
            coinText.text = "Gold: " + playerCoin;
        }
        else
        {
            Debug.LogError("Komponen TextMeshPro Koin Pemain tidak ditetapkan!");
        }
    }
}