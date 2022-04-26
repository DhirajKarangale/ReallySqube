using UnityEngine;

public class Rope : MonoBehaviour
{
    private Rigidbody2D player;
    private Rigidbody2D objectToPull;
    private Reverse reverse;
    private GameManager gameManager;
    [SerializeField] LineRenderer line;
    [SerializeField] bool isMobileInput;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundRope;
    [SerializeField] AudioClip soundHit;

    private Vector3 velocity;
    private bool isRopeGoing;
    private bool isRopeThrown;

    private void Start()
    {
        player = PlayerHealth.instance.playerMove.rigidBody;
        reverse = PlayerHealth.instance.reverse;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (gameManager.isGameOver || reverse.isRewinding || !Shop.instance.isRopeActive)
        {
            DesableLine();
            return;
        }

        GetInputs();

        if (!isRopeThrown)
        {
            DesableLine();
            return;
        }

        if (isRopeGoing) ThrowRope();
        else GetRope();

        ActiveLine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isRopeGoing) return;

        audioSource.Stop();
        audioSource.PlayOneShot(soundHit);
        velocity = Vector3.zero;
        isRopeGoing = false;

        Rigidbody2D collidedObject = collision.GetComponent<Rigidbody2D>();
        if (collidedObject && (collidedObject.mass < 1))
        {
            objectToPull = collidedObject;
            return;
        }
        objectToPull = player;
        CancelInvoke("StopRope");
        Invoke("StopRope", 5);
        return;
    }

    private void GetInputs()
    {
        if (isMobileInput)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.tapCount > 1)
                {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchDir = touchPos - player.position;
                    touchDir = touchDir.normalized;
                    SetRopeDir(touchDir);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchDir = touchPos - player.position;
                touchDir = touchDir.normalized;
                SetRopeDir(touchDir);
            }
        }
    }

    private void ThrowRope()
    {
        transform.position += velocity * Time.deltaTime;
        if (Vector2.Distance(transform.position, player.position) > 40) StopRope();
    }

    private void GetRope()
    {
        Vector2 pullDirection = (Vector2)transform.position - player.position;
        pullDirection = pullDirection.normalized;
        float pullForce = 30;

        // Pull Object towards player
        if (objectToPull != player)
        {
            pullForce = -10;
            transform.position = objectToPull.position;
        }

        objectToPull.AddForce(pullDirection * pullForce);
        // if (Vector2.Distance(transform.position, player.position) < 2f) StopPlayer();
    }

    private void SetRopeDir(Vector2 dir)
    {
        audioSource.clip = soundRope;
        if (!audioSource.isPlaying) audioSource.Play();
        velocity = dir * 60; //Rope Velocity
        transform.position = player.position + dir;
        isRopeGoing = true;
        isRopeThrown = true;
    }

    private void StopPlayer()
    {
        player.velocity = Vector2.zero;
        StopRope();
    }

    private void StopRope()
    {
        DesableLine();
        isRopeThrown = false;
    }

    private void DesableLine()
    {
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        line.enabled = false;
    }

    private void ActiveLine()
    {
        line.enabled = true;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, player.position);
    }
}