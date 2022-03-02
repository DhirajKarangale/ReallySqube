using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] float radius, damage, explosionForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 2);
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearByObj in colliders)
        {
            if (nearByObj.CompareTag("Player"))
            {
                // Add Force
                Vector2 dir = (Vector2)nearByObj.transform.position - (Vector2)transform.position;
                // Vector2 dir = new Vector2(collision.transform.position.y, collision.transform.position.x) - new Vector2(transform.position.y, transform.position.x);
                nearByObj.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce);

                // Add Damage
                nearByObj.gameObject.GetComponent<PlayerHealth>().ChangeHealth(damage);

                break;
            }
        }
        Destroy(gameObject);
    }
}
