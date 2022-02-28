using UnityEngine;

public class DistSound : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] Transform player;
    [SerializeField] AudioSource sound;

    private void FixedUpdate()
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
