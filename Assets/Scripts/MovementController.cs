using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction = Vector2.right;
    public LayerMask targetLayer;
    public LayerMask baseLayer;

    private Rigidbody2D rb;
    private bool isMoving = true;
    private DamageController damageController;
    private Animator anim; // **TAMBAHAN**: Variabel untuk Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        damageController = GetComponent<DamageController>();
        if (damageController == null)
        {
            Debug.LogError(gameObject.name + " Komponen DamageController tidak ditemukan dalam objek!");
        }

        anim = GetComponent<Animator>(); // **TAMBAHAN**: Mengambil komponen Animator
    }

    void FixedUpdate()
    {
        // **TAMBAHAN**: Atur parameter IsMoving di Animator setiap frame
        // Ini memastikan animasi selalu sinkron dengan status gerakan.
        if (anim != null)
        {
            anim.SetBool("IsMoving", isMoving);
        }
        
        if (isMoving)
        {
            rb.linearVelocity = direction * speed; // Menggunakan .velocity lebih umum untuk Rigidbody2D
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    int layerMask = 1 << collision.gameObject.layer;

    if ((layerMask & targetLayer) != 0)
    {
        isMoving = false;
    }

    // if ((layerMask & baseLayer) != 0)
    // {
    //     direction = -direction; // Membalik arah
    // }
}


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = false; // Tetap berhenti selama bersentuhan
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            isMoving = true; // Lanjutkan gerakan setelah tidak bersentuhan
        }
    }
}