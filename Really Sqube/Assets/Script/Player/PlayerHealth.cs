using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Health")]
    // [SerializeField] float health;
    [SerializeField] Slider healthSlider;
    public GameObject effects;
    private float currHealth;

    [Header("Effect")]
    [SerializeField] ParticleSystem psDie;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.MinMaxGradient originalPsColor;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageClip;
    [SerializeField] AudioClip healthClip;

    private void Awake()
    {
        instance = this;

        currHealth = 100;
        UpdateHealthBar();
        psMain = psDie.main;
        originalPsColor = psMain.startColor;
    }

    public void ChangeHealth(float damage)
    {
        if(GameManager.instance.isGameOver)
        {
            this.enabled = false;
            return;
        }
        currHealth = Mathf.Clamp(currHealth -= damage, -1, 100 + 1);
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
        }
    }

    public void PS(byte burstAmount)
    {
        ParticleSystem.EmissionModule psModule = psDie.emission;
        psModule.SetBurst(0, new ParticleSystem.Burst(0, burstAmount));
        psDie.Play();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currHealth / 100;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.yellow, healthSlider.normalizedValue);
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }
}
