using UnityEngine;

public class ReturnObj : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < -100)
        {
            transform.position = PlayerHealth.instance.transform.position;
        }
    }
}
