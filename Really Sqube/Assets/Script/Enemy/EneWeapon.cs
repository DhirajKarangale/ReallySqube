using UnityEngine;

public class EneWeapon : MonoBehaviour
{
    public float damage;
    [SerializeField] float impactForce;
    [SerializeField] bool isThrow;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerHealth.reverse.isRewinding) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.ChangeHealth(damage);
            Vector2 forceDir = collision.transform.position - transform.position;
            forceDir = forceDir.normalized;

            if (Shop.instance.isAgentActive && enemyHealth)
            {
                enemyHealth.TakeDamage(100);
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * impactForce * 2f);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * impactForce);
            }
        }
        if (isThrow)
        {
            enemyHealth.TakeDamage(100);
        }
    }
}
