using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource soundButton;
    [SerializeField] AudioSource soundSpwan;

    private void Start()
    {
        soundSpwan.Play();
    }

    public void RestartButton()
    {
        soundButton.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
