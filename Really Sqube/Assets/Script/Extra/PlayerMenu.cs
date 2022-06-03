using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] ParticleSystem psFall;
    [SerializeField] ParticleSystem psJump;

    [Header("Jump")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] float jumpForce;

    [Header("Petrol")]
    [SerializeField] float leftPos;
    [SerializeField] float rightPos;
    [SerializeField] float rate;
    [SerializeField] float speed;
    private Vector2 movePos;
    private bool moveRight;

    private void Start()
    {
        movePos.y = transform.position.y;
        movePos.x = leftPos;
        InvokeRepeating("ChangeDirection", 0, rate);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos, speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        psFall.Play();
        rigidBody.AddForce(Vector2.up * jumpForce);
        psJump.Play();
    }

    private void ChangeDirection()
    {
        if (transform.position.x == leftPos)
        {
            movePos.x = rightPos;
            transform.localScale = Vector3.one;
        }
        else if (transform.position.x == rightPos)
        {
            movePos.x = leftPos;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
