using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject controlUI;
    [SerializeField] GameObject gameoverUI;
    public bool isGameOver;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip spwanClip;
    [SerializeField] AudioClip gameoverClip;

    private void Awake()
    {
        instance = this;
        isGameOver = false;

        if(controlUI) controlUI.SetActive(true);
        if(gameoverUI) gameoverUI.SetActive(false);
        if(spwanClip) PlaySound(spwanClip);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    private void ActiveGameOverUI()
    {
        gameoverUI.SetActive(true);
        PlaySound(gameoverClip);
        Destroy(PlayerHealth.instance.gameObject);
    }



    public void GameOver()
    {
        isGameOver = true;

        Instantiate(deathEffect, PlayerHealth.instance.transform.position, Quaternion.identity);
        controlUI.SetActive(false);
        Invoke("ActiveGameOverUI", 2);

        PlayerHealth.instance.effects.SetActive(false);
        PlayerHealth.instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        PlayerHealth.instance.gameObject.GetComponent<PlayerMove>().enabled = false;
        PlayerHealth.instance.gameObject.GetComponent<PlayerHealth>().enabled = false;
        PlayerHealth.instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }



    public void MenuButton()
    {
        PlaySound(buttonClip);
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        PlaySound(buttonClip);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
