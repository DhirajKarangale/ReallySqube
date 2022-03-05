using UnityEngine;

public class DoorButton : MonoBehaviour
{
    private bool isBoxCollided;
    [SerializeField] float fadeTime;
    [SerializeField] float doorSpeed;
    [SerializeField] Vector2 doorOpenPos;
    [SerializeField] Transform door;

    private void Update()
    {
        if (isBoxCollided)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 0, 1), fadeTime * Time.deltaTime);
            door.position = Vector3.MoveTowards(door.position, doorOpenPos, doorSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && !isBoxCollided)
        {
            isBoxCollided = true;
        }
    }
}
