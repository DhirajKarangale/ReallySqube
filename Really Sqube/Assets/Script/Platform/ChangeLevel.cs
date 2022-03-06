using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] float dist;
    [SerializeField] float fadeTime;
    [SerializeField] AudioSource soundChangeLevel;

    private void Update()
    {
        if (PlayerHealth.instance && dist >= Vector2.Distance(transform.position, PlayerHealth.instance.transform.position))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, -0.1f, 1), fadeTime * Time.deltaTime);
            if (transform.localScale.y <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                this.enabled = false;
            }
        }
    }
}
