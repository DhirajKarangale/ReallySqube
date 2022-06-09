using UnityEngine;

public class ReturnObj : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = PlayerHealth.instance.transform;
    }

    private void Update()
    {
        if (transform.position.y < -100)
        {
            transform.position = player.position;
        }
    }
}
