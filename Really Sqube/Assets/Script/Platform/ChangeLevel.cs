using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] float fadeTime;
    [SerializeField] AudioSource soundChangeLevel;
    private bool isPlayerClose;

    private void Update()
    {
        if (PlayerHealth.instance && dist >= Mathf.Abs(Vector2.Distance(transform.position, PlayerHealth.instance.transform.position)))
        {
            isPlayerClose = true;
        }

        if (isPlayerClose)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, -0.1f, 1), fadeTime * Time.deltaTime);
            if (!soundChangeLevel.isPlaying) soundChangeLevel.Play();
            if (transform.localScale.y <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                this.enabled = false;
            }
        }
    }
}
