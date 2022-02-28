using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Refrence")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Animator animator;

    [Header("Jump")]
    [SerializeField] float jumpVel = 5;
    [SerializeField] float jumpPressTime = 0.2f;
    [SerializeField] float groundTime = 0.25f;
    [Range(0, 1)] [SerializeField] float jumpFallVel = 0.5f;
    private float ground = 0;
    private float jumpPress = 0;
    private bool isJumpButton;
    private bool isAir;

    [Header("Move")]
    [Range(0, 1)] [SerializeField] float moveDamping = 0.5f;
    [Range(0, 1)] [SerializeField] float stopDamping = 0.5f;
    [Range(0, 1)] [SerializeField] float turnDamping = 0.5f;
    private float moveInputVal;

    [Header("Effect")]
    [SerializeField] ParticleSystem psJump;
    [SerializeField] ParticleSystem psLand;

    [Header("Sound")]
    [SerializeField] AudioSource soundJump;
    [SerializeField] AudioSource soundFall;
    [SerializeField] AudioSource soundMove;

    public bool isKeyboard;

    private void Update()
    {
        // Remove This If Else for Final bilding ↓↓
        if (isKeyboard)
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
                PlaySound(soundJump);
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
        }
        else
        {
            Jump();
            Move();
        }
        // Remove This If Else for Final Building ↑↑

        // Jump();
        // Move();
        Flip();
        Landed();
        CheckMoving();

        if ((transform.position.y <= -100) && (rigidBody.constraints != RigidbodyConstraints2D.FreezeAll))
        {
            PlayerHealth.instance.Die();
        }
    }

    private void Jump()
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
            jumpPress = 0;
            ground = 0;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVel);
            psJump.Play();
            PlaySound(soundJump);
        }
    }

    private void Move()
    {
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

    private void Flip()
    {
        if (Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), 1);
        }
    }

    private void Landed()
    {
        if (isAir && IsGrounded())
        {
            psLand.Play();
            PlaySound(soundFall);
            isAir = false;
        }

        if (!IsGrounded()) isAir = true;
    }

    private void CheckMoving()
    {
        if (Mathf.Abs(rigidBody.velocity.x) > 0)
        {
            animator.Play("Move");
            if (IsGrounded())
            {
                if (!soundMove.isPlaying) soundMove.Play();
            }
            else
            {
                soundMove.Stop();
            }
        }
        else
        {
            animator.Play("Idel");
            soundMove.Stop();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.IsTouchingLayers(boxCollider);
    }

    private void PlaySound(AudioSource sound)
    {
        if (sound.isPlaying) sound.Stop();
        sound.Play();
    }



    public void DownButton()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, -5);
    }

    public void MoveButton(int value)
    {
        moveInputVal += value;
    }

    public void JumpButton(bool isJump)
    {
        if (boxCollider.isTrigger)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y + 200);
        }
        else
        {
            isJumpButton = isJump;
        }
    }
}
