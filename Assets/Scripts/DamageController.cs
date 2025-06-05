using UnityEngine;

public class DamageController : MonoBehaviour
{
    public float damage = 10f;
    public float attackInterval = 1f;
    private float attackTimer = 0f;
    public LayerMask targetLayer; // **INSPECTOR'DAN AYARLANMALI (EnemyUnit ve Base Layer'ları)**

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attackTimer >= attackInterval && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            HealthSystem targetHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
                attackTimer = 0f;
            }
        }
        attackTimer += Time.deltaTime;
    }
}