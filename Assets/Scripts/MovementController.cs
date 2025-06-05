using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private bool isMoving = true;
    public LayerMask targetLayer; // **INSPECTOR'DAN AYARLANMALI (EnemyUnit ve Base Layer'ları)**
    private DamageController damageController; // DamageController bileşenini tutacak değişken

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageController = GetComponent<DamageController>(); // Kendi üzerindeki DamageController'ı al
        if (damageController == null)
        {
            Debug.LogError(gameObject.name + " objesinde DamageController bileşeni bulunamadı!");
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Dur
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero; // Anında dur
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero; // Temas sürdükçe dur
            // DamageController'daki OnCollisionStay2D zaten hasar veriyor.
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (damageController != null && ((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = true; // Temas kesildiğinde hareket etmeye devam et
        }
    }
}