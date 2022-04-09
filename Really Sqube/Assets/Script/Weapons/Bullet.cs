using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject impactEffect;
    [SerializeField] float radius, damage, explosionForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(Instantiate(impactEffect, transform.position, Quaternion.identity), 2);

        if (GameManager.instance.isGameOver)
        {
            Destroy(gameObject);
            return;
        }
        if (Mathf.Abs(Vector2.Distance(transform.position, PlayerHealth.instance.transform.position)) < radius)
        {
            Vector2 dir = (Vector2)PlayerHealth.instance.transform.position - (Vector2)transform.position;
            PlayerHealth.instance.playerMove.rigidBody.AddForce(dir * explosionForce);

            // Add Damage
            PlayerHealth.instance.ChangeHealth(damage);
            CamShake.instance.Shake(7.5f, 0.15f);
        }

        Destroy(gameObject);
    }
}
