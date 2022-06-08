using UnityEngine;
using UnityEngine.EventSystems;

public class Rope : MonoBehaviour
{
    private Rigidbody2D player;
    private Rigidbody2D objectToPull;
    private Reverse reverse;
    private GameManager gameManager;

    [SerializeField] LineRenderer line;
    [SerializeField] bool isMobileInput;

    [Header("Attributes")]
    [SerializeField] float ropeTime = 10;
    [SerializeField] float ropeDist = 40;
    [SerializeField] float ropeVelocity = 60;
    [SerializeField] float playerPullForce = 30;
    [SerializeField] float objectPullForce = 20;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip soundRope;
    [SerializeField] AudioClip soundHit;

    private Vector3 velocity;
    private bool isRopeGoing;
    private bool isRopeThrown;
    // private bool isButtonPressed;

    private Transform collidedObjPos;
    private Vector3 collidedObjOldPos;

    private void Start()
    {
        player = PlayerHealth.instance.playerMove.rigidBody;
        reverse = PlayerHealth.instance.reverse;
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        if (gameManager.isGameOver || reverse.isRewinding || !Shop.instance.isRopeActive || (Time.timeScale == 0) || DialogueManager.instance.isPlayerStop)
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
        collidedObjPos = collision.transform;
        collidedObjOldPos = collision.transform.position;
        CancelInvoke("StopRope");
        Invoke("StopRope", ropeTime);
        return;
    }

    private void GetInputs()
    {
        if (isMobileInput)
        {
            if (Input.touchCount > 0)
            {
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.tapCount > 1)
                        {
                            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                            Vector2 touchDir = touchPos - player.position;
                            touchDir = touchDir.normalized;
                            SetRopeDir(touchDir);
                        }
                    }
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
        if (Vector2.Distance(transform.position, player.position) > ropeDist) StopRope();
    }

    private void GetRope()
    {
        if (objectToPull == player)
        {
            if (collidedObjPos)
            {
                if (collidedObjOldPos != collidedObjPos.position)
                {
                    transform.position = collidedObjPos.position;
                }
            }
            else
            {
                StopPlayer();
                return;
            }
        }

        Vector2 pullDirection = (Vector2)transform.position - player.position;
        pullDirection = pullDirection.normalized;
        float pullForceTemp = playerPullForce;

        // Pull Object towards player
        if (!objectToPull)
        {
            StopRope();
            return;
        }
        if (objectToPull != player)
        {
            pullForceTemp = -objectPullForce;
            transform.position = objectToPull.position;
        }

        objectToPull.AddForce(pullDirection * pullForceTemp);
    }

    private void SetRopeDir(Vector2 dir)
    {
        audioSource.clip = soundRope;
        if (!audioSource.isPlaying) audioSource.Play();
        velocity = dir * ropeVelocity;
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