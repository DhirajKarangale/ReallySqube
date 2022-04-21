using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private PlayerHealth player;

    [SerializeField] Rigidbody2D rigidBody;

    [Header("Attributes")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float followDist;
    private float playerDist;

    [Header("Petrol")]
    [SerializeField] float leftPos;
    [SerializeField] float rightPos;
    private Vector2 movePos;
    private bool moveRight;

    private void Start()
    {
        player = PlayerHealth.instance;
        movePos = transform.position;
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver || player.reverse.isRewinding) return;

        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.instance.isGameOver) return;
        rigidBody.AddForce(Vector2.up * jumpForce);
    }

    private void Move()
    {
        playerDist = Mathf.Abs(Vector2.Distance(transform.position, player.transform.position));
        if (playerDist <= followDist) FollowPlayer();
        else Petrol();
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed * 2);
    }

    private void Petrol()
    {
        if (transform.position.x >= rightPos) moveRight = false;     // Move Back
        else if (transform.position.x <= leftPos) moveRight = true;  // Move Front

        movePos.x = moveRight ? rightPos : leftPos;
        transform.position = Vector2.MoveTowards(transform.position, movePos, Time.deltaTime * speed);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector2(leftPos, transform.position.y), 1);
        Gizmos.DrawSphere(new Vector2(rightPos, transform.position.y), 1);
        Gizmos.DrawWireSphere(transform.position, followDist);
    }
}
