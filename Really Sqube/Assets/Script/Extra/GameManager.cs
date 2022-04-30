using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] ParticleSystem psDie;
    private ParticleSystem.MainModule psMain;
    private float playerStartDir;
    private UIManager uiManager;
    private PlayerHealth playerHealth;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClip;
    [SerializeField] AudioClip spwanClip;
    [SerializeField] AudioClip gameoverClip;
    public bool isGameOver;

    private void Awake()
    {
        instance = this;
        isGameOver = false;
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        playerHealth = PlayerHealth.instance;

        if (PlayerPrefs.GetInt("Level", 1) <= SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        }

        if (spwanClip) PlaySound(spwanClip);
        psMain = psDie.main;
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    private void ActiveGameOverUI()
    {
        uiManager.objGameOver.SetActive(true);
        PlaySound(gameoverClip);
    }


    public void StopPlayer(float playerDir)
    {
        if (!uiManager.objButtons.activeInHierarchy) return;
        playerHealth.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        playerHealth.playerMove.moveInputVal = 0;
        playerStartDir = playerDir;
        uiManager.objButtons.SetActive(false);
    }

    public void StartPlayer()
    {
        if (uiManager.objButtons.activeInHierarchy) return;
        playerHealth.playerMove.rigidBody.constraints = RigidbodyConstraints2D.None;
        playerHealth.playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerHealth.playerMove.rigidBody.AddForce(Vector2.right * playerStartDir);
        uiManager.objButtons.SetActive(true);
    }

    public void GameOver()
    {
        if (!playerHealth.isStatus) return;

        uiManager.buttonOverReverse.SetActive(CollectableData.instance.timeStone >= 4);
        uiManager.buttonOverReverseAD.SetActive(CollectableData.instance.timeStone < 4);

        isGameOver = true;
        CamShake.instance.Shake(8f, 0.2f);
        // Handheld.Vibrate();

        psMain.startColor = playerHealth.originalPsColor;
        Instantiate(psDie.gameObject, playerHealth.transform.position, Quaternion.identity);
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(false);
        playerHealth.Status(false);
        uiManager.objButtons.SetActive(false);
        uiManager.objUI.SetActive(false);

        Invoke("ActiveGameOverUI", 2);
    }

    public void RespwanPlayer()
    {
        isGameOver = false;
        if (DialogueManager.instance) DialogueManager.instance.gameObject.SetActive(true);
        playerHealth.Status(true);
        uiManager.objButtons.SetActive(true);
        uiManager.objUI.SetActive(true);
        uiManager.objGameOver.SetActive(false);
    }
}
