using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector2 pos1, pos2;
    [SerializeField] AudioSource soundPlatform;
    private Vector2 nextPos;

    private void Start()
    {
        nextPos = pos1;
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if ((Vector2)transform.position == pos1) nextPos = pos2;
        else if ((Vector2)transform.position == pos2) nextPos = pos1;
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
            if(!soundPlatform.isPlaying) soundPlatform.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            soundPlatform.Stop();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pos1, 1);
        Gizmos.DrawSphere(pos2, 1);
    }
}