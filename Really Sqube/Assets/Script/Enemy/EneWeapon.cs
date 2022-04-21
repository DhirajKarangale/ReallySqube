using UnityEngine;

public class EneWeapon : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float impactForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(PlayerHealth.instance.reverse.isRewinding) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.ChangeHealth(damage);
            // Vector2 forceDir = new Vector2(collision.transform.position.x - transform.position.x, 0.7f);
            Vector2 forceDir = collision.transform.position - transform.position;
            forceDir = forceDir.normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDir * impactForce);
        }
    }
}
