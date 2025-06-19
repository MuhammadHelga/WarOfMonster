using System.Collections;
using UnityEngine;

public class EnemyCoinController : MonoBehaviour
{
    public int enemyCoin = 20;
    public static EnemyCoinController instance;

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

    public void GainCoin(int amount)
    {
        enemyCoin += amount;
    }

    public bool CanAfford(int amount)
    {
        return enemyCoin >= amount;
    }

    public void SpendCoin(int amount)
    {
        if (CanAfford(amount))
        {
            enemyCoin -= amount;
        }
    }

    public int GetCurrentCoin()
    {
        return enemyCoin;
    }
}