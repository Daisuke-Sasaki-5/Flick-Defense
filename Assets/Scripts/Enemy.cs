using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fallSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        // ‰æ–ÊŠO‚Éo‚½‚çíœ
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            Debug.Log("“–‚½‚Á‚½");
            GameManager.instance.DamageBase(1);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Bullet"))
        {
            GameManager.instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}
