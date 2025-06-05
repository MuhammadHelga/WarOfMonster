using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private bool isMoving = true;
    public LayerMask targetLayer;
    private DamageController damageController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevent rotation upon collision

        damageController = GetComponent<DamageController>();
        if (damageController == null)
        {
            Debug.LogError(gameObject.name + " objesinde DamageController bileşeni bulunamadı!");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = direction * speed; // Corrected linear velocity to velocity
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop immediately
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero; // Stop on collision
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero; // Stop while in contact
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = true; // Resume movement after collision
        }
    }
}
