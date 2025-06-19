using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour
{
    public float damage = 10f;
    public float attackInterval = 1f;
    public LayerMask targetLayer;

    private Coroutine attackCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek layer target
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            attackCoroutine = StartCoroutine(AttackRepeatedly(collision.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    // private IEnumerator AttackRepeatedly(GameObject target)
    // {
    //     while (true)
    //     {
    //         HealthSystem health = target.GetComponent<HealthSystem>();
    //         if (health != null)
    //         {
    //             health.TakeDamage(damage);
    //         }

    //         yield return new WaitForSeconds(attackInterval);
    //     }
    // }

    private IEnumerator AttackRepeatedly(GameObject target)
{
    while (target != null)
    {
        if (target.TryGetComponent(out HealthSystem health))
        {
            health.TakeDamage(damage);
        }
        else
        {
            yield break;
        }

        yield return new WaitForSeconds(attackInterval);
    }
}

}
