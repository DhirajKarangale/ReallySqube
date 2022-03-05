using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float force;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 dir = (Vector2)collision.transform.position - (Vector2)transform.position;
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb) rb.AddForce(Vector2.up * force);
    }
}
