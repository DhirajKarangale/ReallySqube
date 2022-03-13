using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] float radius, damage, explosionForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.isGameOver) return;

        Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 2);

        if (Mathf.Abs(Vector2.Distance(transform.position, PlayerHealth.instance.transform.position)) < radius)
        {
            Vector2 dir = (Vector2)PlayerHealth.instance.transform.position - (Vector2)transform.position;
            // Vector2 dir = new Vector2(collision.transform.position.y, collision.transform.position.x) - new Vector2(transform.position.y, transform.position.x);
            PlayerHealth.instance.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce);

            // Add Damage
            PlayerHealth.instance.ChangeHealth(damage);
        }

        Destroy(gameObject);
    }
}
