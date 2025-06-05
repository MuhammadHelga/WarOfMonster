using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSpawner : MonoBehaviour
{
    public GameObject weakSoldierPrefabPlayer;
    public int weakSoldierCostPlayer = 10;
    public GameObject strongSoldierPrefabPlayer;
    public int strongSoldierCostPlayer = 25;
    public Transform playerSpawnPoint;

    public GameObject weakSoldierPrefabEnemy;
    public int weakSoldierCostEnemy = 15;
    public GameObject strongSoldierPrefabEnemy;
    public int strongSoldierCostEnemy = 30;
    public Transform enemySpawnPoint;

    public float enemySpawnInterval = 2f;
    private float enemySpawnTimer = 0f;

    public Button weakSoldierButtonPlayer;
    public Button strongSoldierButtonPlayer;

    void Start()
    {
        if (weakSoldierButtonPlayer != null)
        {
            weakSoldierButtonPlayer.onClick.AddListener(SpawnWeakSoldierPlayer);
        }
        else
        {
            Debug.LogError("Zayıf asker butonu atanmamış!");
        }

        if (strongSoldierButtonPlayer != null)
        {
            strongSoldierButtonPlayer.onClick.AddListener(SpawnStrongSoldierPlayer);
        }
        else
        {
            Debug.LogError("Güçlü asker butonu atanmamış!");
        }
    }

    void Update()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnInterval)
        {
            SpawnEnemyUnit();
            enemySpawnTimer = 0f;
        }
    }

    public void SpawnWeakSoldierPlayer()
    {
        if (PlayerCoinController.instance.CanAfford(weakSoldierCostPlayer))
        {
            PlayerCoinController.instance.SpendCoin(weakSoldierCostPlayer);
            Instantiate(weakSoldierPrefabPlayer, playerSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Oyuncu: Yeterli altın yok!");
            if (weakSoldierButtonPlayer != null) weakSoldierButtonPlayer.interactable = false;
            Invoke("EnableWeakSoldierButton", 1f);
        }
    }

    void EnableWeakSoldierButton()
    {
        if (weakSoldierButtonPlayer != null) weakSoldierButtonPlayer.interactable = true;
    }

    public void SpawnStrongSoldierPlayer()
    {
        if (PlayerCoinController.instance.CanAfford(strongSoldierCostPlayer))
        {
            PlayerCoinController.instance.SpendCoin(strongSoldierCostPlayer);
            Instantiate(strongSoldierPrefabPlayer, playerSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Oyuncu: Yeterli altın yok!");
            if (strongSoldierButtonPlayer != null) strongSoldierButtonPlayer.interactable = false;
            Invoke("EnableStrongSoldierButton", 1f);
        }
    }

    void EnableStrongSoldierButton()
    {
        if (strongSoldierButtonPlayer != null) strongSoldierButtonPlayer.interactable = true;
    }

    public void SpawnEnemyUnit()
    {
        int currentEnemyCoin = EnemyCoinController.instance.GetCurrentCoin();
        float randomValue = Random.value;

        if (currentEnemyCoin >= strongSoldierCostEnemy && randomValue > 0.5f)
        {
            EnemyCoinController.instance.SpendCoin(strongSoldierCostEnemy);
            Instantiate(strongSoldierPrefabEnemy, enemySpawnPoint.position, Quaternion.identity);
        }
        else if (currentEnemyCoin >= weakSoldierCostEnemy)
        {
            EnemyCoinController.instance.SpendCoin(weakSoldierCostEnemy);
            Instantiate(weakSoldierPrefabEnemy, enemySpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Düşman: Yeterli altın yok veya rastgele seçim başarısız.");
        }
    }
}