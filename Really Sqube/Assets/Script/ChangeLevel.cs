using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    private bool isPlayerCollided;
    [SerializeField] float fadeTime;
    [SerializeField] AudioSource soundChangeLevel;

    private void Update()
    {
        if (isPlayerCollided)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.3f, 0, 0.3f), fadeTime * Time.deltaTime);
            Invoke("Level", 2.5f);
        }
    }

    private void Level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isPlayerCollided = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerCollided = true;
            if (soundChangeLevel.isPlaying) soundChangeLevel.Stop();
            soundChangeLevel.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
