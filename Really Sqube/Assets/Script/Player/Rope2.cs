using UnityEngine;

public class Rope2 : MonoBehaviour
{
    [SerializeField] Rigidbody2D origin;
    [SerializeField] LineRenderer line;
    private Vector3 velocity;
    private bool isPullRope = false;
    private bool isRopeActive = false;
    private Rigidbody2D objectToPull;
    [SerializeField] AudioSource rope;

    private void Update()
    {
        ActiveRope();
        if (!isRopeActive) return;

        if (isPullRope)
        {
            float pullForce = 100; // 1500 is pullForce
            Vector2 pullDirection = (Vector2)transform.position - origin.position;
            pullDirection = pullDirection.normalized;
            if (objectToPull != origin)
            {
                pullForce = -3; // -pullForce/50
                transform.position = objectToPull.position;
            }
            if (Mathf.Abs(transform.position.x - origin.position.x) < 2f)
            {
                objectToPull.velocity = Vector2.zero;
                DeactiveRope();
                return;
            }
            objectToPull.AddForce(pullDirection * pullForce);
        }
        else
        {
            transform.position += velocity * Time.deltaTime;
            float distance = Vector2.Distance(transform.position, origin.position);
            if (distance > 30)
            {
                DeactiveRope();
                return;
            }
        }

        line.SetPosition(0, transform.position);
        line.SetPosition(1, origin.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        rope.Stop();
        // AudioManager.instance.Play("Hit");
        velocity = Vector3.zero;
        Rigidbody2D rigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rigidBody && (rigidBody.mass < 1.5f)) objectToPull = rigidBody;
        else objectToPull = origin;

        isPullRope = true;
    }

    private void SetRope(Vector2 targetPos)
    {
        rope.Play();
        Vector2 throughDirection = targetPos;
        throughDirection = throughDirection.normalized;
        velocity = throughDirection * 75; // 75 is speed
        transform.position = origin.position + throughDirection;
        isPullRope = false;
        isRopeActive = true;
    }

    public void ActiveRope()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount >= 2)
            {
                CancelInvoke("DesableRope");
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos = touchPos.normalized;
                SetRope(touchPos * 100);
                Debug.Log(touchPos);
                Invoke("DesableRope", 3);
            }
        }
    }

    private void DesableRope()
    {
        objectToPull.velocity = Vector2.zero;
        DeactiveRope();
    }

    public void DeactiveRope()
    {
        isRopeActive = false;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
    }
}