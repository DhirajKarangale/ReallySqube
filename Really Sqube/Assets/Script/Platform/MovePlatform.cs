using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float rate;
    [SerializeField] float speed;
    [SerializeField] Vector2 pos1, pos2;
    private Vector2 nextPos;

    private void Start()
    {
        nextPos = pos1;
        InvokeRepeating("Move", 0, rate);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        // rigidBody.position = Vector2.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void Move()
    {
        if ((Vector2)transform.position == pos1) nextPos = pos2;
        else if ((Vector2)transform.position == pos2) nextPos = pos1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 1);
        Gizmos.DrawSphere(pos2, 1);
    }
}
