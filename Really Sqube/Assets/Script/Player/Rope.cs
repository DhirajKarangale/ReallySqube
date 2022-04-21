using UnityEngine;

public class Rope : MonoBehaviour
{
    private Rigidbody2D player;
    [SerializeField] LineRenderer line;
    [SerializeField] bool isMobileInput;

    private Vector3 velocity;
    private bool isRopeGoing;
    private bool isRopeThrown;

    private void Start()
    {
        player = PlayerHealth.instance.playerMove.rigidBody;
    }

    private void Update()
    {
        GetInputs();

        if (!isRopeThrown)
        {
            DesableLine();
            return;
        }

        if (isRopeGoing) ThrowRope();
        else PullPlayer();

        ActiveLine();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        velocity = Vector3.zero;
        isRopeGoing = false;
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
                    CancelInvoke("StopRope");
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchDir = touchPos - player.position;
                    touchDir = touchDir.normalized;
                    SetRopeDir(touchDir);
                    Invoke("StopRope", 5);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                CancelInvoke("StopRope");
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchDir = touchPos - player.position;
                touchDir = touchDir.normalized;
                SetRopeDir(touchDir);
                Invoke("StopRope", 5);
            }
        }
    }

    private void ThrowRope()
    {
        transform.position += velocity * Time.deltaTime;
        if (Vector2.Distance(transform.position, player.position) > 40) StopRope();
    }

    private void PullPlayer()
    {
        Vector2 pullDirection = (Vector2)transform.position - player.position;
        pullDirection = pullDirection.normalized;
        player.AddForce(pullDirection * 40); //Pull Force
        // if (Vector2.Distance(transform.position, player.position) < 2f) StopPlayer();
    }

    private void SetRopeDir(Vector2 dir)
    {
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