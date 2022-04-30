using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public PlayerMove playerMove;
    public Reverse reverse;
    private UIManager uiManager;

    [Header("Health")]
    [SerializeField] float health;
    private float currHealth;
    [HideInInspector] public bool isStatus;

    [Header("Effect")]
    public ParticleSystem psDie;
    public ParticleSystem psUpgrade;
    public SpriteRenderer gui;
    private ParticleSystem.MainModule psMain;
    [HideInInspector] public ParticleSystem.MinMaxGradient originalPsColor;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip healthClip;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        isStatus = true;
        currHealth = health;
        UpdateHealthBar();
        psMain = psDie.main;
        originalPsColor = psMain.startColor;
    }

    public void ChangeHealth(float damage)
    {
        if (GameManager.instance.isGameOver)
        {
            this.enabled = false;
            return;
        }
        if (Shop.instance.isAgentActive) damage = damage / 2;
        currHealth = Mathf.Clamp(currHealth -= damage, 0, health + 1);
        UpdateHealthBar();

        if (currHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
        else if (damage < 0)
        {
            psMain.startColor = Color.green;
            PlaySound(healthClip);
            PS(15);
        }
        else
        {
            psMain.startColor = originalPsColor;
            PlaySound(damageClip);
            PS(5);
            CamShake.instance.Shake();
            // Handheld.Vibrate();
        }
    }

    public void PS(byte burstAmount)
    {
        ParticleSystem.EmissionModule psModule = psDie.emission;
        psModule.SetBurst(0, new ParticleSystem.Burst(0, burstAmount));
        psDie.Play();
    }

    public void Status(bool status)
    {
        gui.gameObject.SetActive(status);
        if (!status)
        {
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.None;
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.position = new Vector3(transform.position.x, -90, 0);
            ChangeHealth(-health);
        }
        playerMove.boxCollider.enabled = status;
        isStatus = status;
    }

    private void UpdateHealthBar()
    {
        uiManager.healthSlider.value = currHealth / health;
        uiManager.healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.yellow, uiManager.healthSlider.normalizedValue);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
