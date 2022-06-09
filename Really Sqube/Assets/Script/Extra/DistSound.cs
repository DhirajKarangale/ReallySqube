using UnityEngine;

public class DistSound : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] AudioSource sound;
    private Transform player;

    private void Start()
    {
        player = PlayerHealth.instance.transform;
    }

    private void Update()
    {
        if (player && dist >= Vector2.Distance(transform.position, player.position))
        {
            if (!sound.isPlaying) sound.Play();
        }
        else
        {
            sound.Stop();
        }
    }
}
