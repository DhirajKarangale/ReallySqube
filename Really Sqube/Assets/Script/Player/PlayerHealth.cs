using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public PlayerMove playerMove;

    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] Slider healthSlider;
    private float currHealth;
    [HideInInspector] public bool isStatus;

    [Header("Effect")]
    [SerializeField] ParticleSystem psDie;
    [SerializeField] GameObject gui;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.MinMaxGradient originalPsColor;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip healthClip;

    private void Awake()
    {
        instance = this;

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
        currHealth = Mathf.Clamp(currHealth -= damage, -1, health + 1);
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
        gui.SetActive(status);
        if (!status)
        {
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.None;
            playerMove.rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        playerMove.boxCollider.enabled = status;
        isStatus = status;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currHealth / health;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.yellow, healthSlider.normalizedValue);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
