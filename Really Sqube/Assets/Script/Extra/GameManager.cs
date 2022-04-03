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

        if (PlayerPrefs.GetInt("Level", 1) <= SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        }

        if (controlUI) controlUI.SetActive(true);
        if (gameoverUI) gameoverUI.SetActive(false);
        if (spwanClip) PlaySound(spwanClip);
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(true);
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
    }


    public void StopPlayer()
    {
        controlUI.SetActive(false);
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        PlayerHealth.instance.playerMove.moveInputVal = 0;
    }

    public void StartPlayer()
    {
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.None;
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        controlUI.SetActive(true);
    }

    public void GameOver()
    {
        isGameOver = true;

        Instantiate(deathEffect, PlayerHealth.instance.transform.position, Quaternion.identity);
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(false);
        Destroy(PlayerHealth.instance.gameObject);
        controlUI.SetActive(false);

        Invoke("ActiveGameOverUI", 2);
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
