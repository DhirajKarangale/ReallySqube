using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource soundButton;
    [SerializeField] AudioSource soundSpwan;

    private void Start()
    {
        if (soundSpwan) soundSpwan.Play();
    }

    public void MenuButton()
    {
        soundButton.Play();
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        soundButton.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
