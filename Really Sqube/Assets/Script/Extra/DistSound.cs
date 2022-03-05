using UnityEngine;

public class DistSound : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] AudioSource sound;

    private void Update()
    {
        if (PlayerHealth.instance && dist >= Vector2.Distance(transform.position, PlayerHealth.instance.transform.position))
        {
            if (!sound.isPlaying) sound.Play();
        }
        else
        {
            sound.Stop();
        }
    }
}
