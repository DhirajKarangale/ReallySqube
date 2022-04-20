using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject deathEffect;
    public bool isGameOver;
    private float playerStartDir;

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

        if (UIManager.instance.objButtons) UIManager.instance.objButtons.SetActive(true);
        if (UIManager.instance.objGameOver) UIManager.instance.objGameOver.SetActive(false);
        if (UIManager.instance.objUI) UIManager.instance.objUI.SetActive(true);
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
        UIManager.instance.objGameOver.SetActive(true);
        PlaySound(gameoverClip);
    }


    public void StopPlayer(float playerDir)
    {
        if (!UIManager.instance.objButtons.activeInHierarchy) return;
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        PlayerHealth.instance.playerMove.moveInputVal = 0;
        playerStartDir = playerDir;
        UIManager.instance.objButtons.SetActive(false);
    }

    public void StartPlayer()
    {
        if (UIManager.instance.objButtons.activeInHierarchy) return;
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.None;
        PlayerHealth.instance.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerHealth.instance.playerMove.rigidBody.AddForce(Vector2.right * playerStartDir);
        UIManager.instance.objButtons.SetActive(true);
    }

    public void GameOver()
    {
        if (!PlayerHealth.instance.isStatus) return;

        isGameOver = true;
        CamShake.instance.Shake(8f, 0.2f);
        // Handheld.Vibrate();

        Instantiate(deathEffect, PlayerHealth.instance.transform.position, Quaternion.identity);
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(false);
        PlayerHealth.instance.Status(false);
        UIManager.instance.objButtons.SetActive(false);
        UIManager.instance.objUI.SetActive(false);

        Invoke("ActiveGameOverUI", 2);
    }

    public void RespwanPlayer()
    {
        isGameOver = false;
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(true);
        PlayerHealth.instance.Status(true);
        UIManager.instance.objButtons.SetActive(true);
        UIManager.instance.objUI.SetActive(true);
        UIManager.instance.objGameOver.SetActive(false);
    }
}
