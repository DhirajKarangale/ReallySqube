using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] AudioSource soundChangeLevel;
    private bool isPlayerCollided;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!soundChangeLevel.isPlaying) soundChangeLevel.Play();
        if (!isPlayerCollided)
        {
            Invoke("PlayerCollided", 0.2f);
            isPlayerCollided = true;
        }
    }

    private void PlayerCollided()
    {
        CollectableData.instance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
