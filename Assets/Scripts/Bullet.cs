using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed = 10f;

    [Header("ÉqÉbÉgSE")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSE;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void Init(Vector2 dir)
    {
        rb.linearVelocity = dir * speed;

        // ï˚å¸Ç…çáÇÌÇπÇƒâÒì]
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            AudioSource.PlayClipAtPoint(hitSE, transform.position);

            Destroy(gameObject);
        }
            
    }
}
