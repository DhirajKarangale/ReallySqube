using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] float speed;

    private void Start()
    {
        playerHealth = PlayerHealth.instance;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerHealth.transform.position, speed * Time.deltaTime);
    }
}
