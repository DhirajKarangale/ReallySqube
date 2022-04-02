using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Refrence")]
    public Rigidbody2D rigidBody;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Animator animator;

    [Header("Jump")]
    [SerializeField] float jumpVel = 5;
    [SerializeField] float jumpPressTime = 0.2f;
    [SerializeField] float groundTime = 0.25f;
    [Range(0, 1)][SerializeField] float jumpFallVel = 0.5f;
    private float ground = 0;
    private float jumpPress = 0;

    [Header("Move")]
    [Range(0, 1)][SerializeField] float moveDamping = 0.5f;
    [Range(0, 1)][SerializeField] float stopDamping = 0.5f;
    [Range(0, 1)][SerializeField] float turnDamping = 0.5f;
    [HideInInspector] public float moveInputVal;

    [Header("Effect")]
    [SerializeField] ParticleSystem psJump;
    [SerializeField] ParticleSystem psLand;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip fallClip;
    // [SerializeField] AudioSource soundMove;

    private void Update()
    {
        Move();
        KeyBoardFunction();

        if (transform.position.y <= -100) GameManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Box"))
        {
            HitEffect();
        }
    }



    private void KeyBoardFunction()
    {
        ground -= Time.deltaTime;
        if (IsGrounded()) ground = groundTime;

        jumpPress -= Time.deltaTime;
        if (Input.GetButtonDown("Jump")) jumpPress = jumpPressTime;

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * jumpFallVel);
        }

        if ((jumpPress > 0) && (ground > 0))
        {
            jumpPress = 0;
            ground = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVel);
            PlaySound(jumpClip);
            psJump.Play();
        }

        float fHorizontalVelocity = rigidBody.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            fHorizontalVelocity *= Mathf.Pow(1f - stopDamping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity))
            fHorizontalVelocity *= Mathf.Pow(1f - turnDamping, Time.deltaTime * 10f);
        else
            fHorizontalVelocity *= Mathf.Pow(1f - moveDamping, Time.deltaTime * 10f);

        rigidBody.velocity = new Vector2(fHorizontalVelocity, rigidBody.velocity.y);

        if (Mathf.Abs(rigidBody.velocity.x) <= Mathf.Epsilon)
        {
            animator.Play("Idel");
        }
        else
        {
            animator.Play("Move");
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1);
        }
    }

    private void Jump(bool isJumpButton)
    {
        ground -= Time.deltaTime;
        if (IsGrounded()) ground = groundTime;

        jumpPress -= Time.deltaTime;
        if (isJumpButton) jumpPress = jumpPressTime;

        if (!isJumpButton && rigidBody.velocity.y > 0)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * jumpFallVel);
        }

        if ((jumpPress > 0) && (ground > 0))
        {
            ground = 0;
            jumpPress = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVel);
            PlaySound(jumpClip);
            psJump.Play();
        }
    }

    private void Move()
    {
        // if (!IsGrounded())
        // {
        //     soundMove.Stop();
        // }

        float xVelocity = rigidBody.velocity.x;
        xVelocity += moveInputVal;

        if (Mathf.Abs(moveInputVal) < 0.01f)
        {
            xVelocity *= Mathf.Pow(1f - stopDamping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(moveInputVal) != Mathf.Sign(xVelocity))
        {
            xVelocity *= Mathf.Pow(1f - turnDamping, Time.deltaTime * 10f);
        }
        else
        {
            xVelocity *= Mathf.Pow(1f - moveDamping, Time.deltaTime * 10f);
        }

        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
    }

    private void HitEffect()
    {
        PlaySound(fallClip);
        psLand.Play();
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (GameManager.instance.isGameOver) return;

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    private bool IsGrounded()
    {
        return Physics2D.IsTouchingLayers(boxCollider);
    }




    public void DownButton()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, -2);
    }

    public void MoveButton(int value)
    {
        moveInputVal += value;
        if (moveInputVal == 0)
        {
            animator.Play("Idel");
            // soundMove.Stop();
        }
        else
        {
            animator.Play("Move");
            transform.localScale = new Vector2(Mathf.Sign(value), 1);
            // if (!soundMove.isPlaying && IsGrounded())
            // {
            //     soundMove.Play();
            // }
        }
    }

    public void JumpButton(bool isJump)
    {
        if (boxCollider.isTrigger)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 2);
        }
        else
        {
            Jump(isJump);
        }
    }
}
