using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("Health")]
    [SerializeField] float health;
    [SerializeField] Slider healthSlider;
    private float currHealth;

    [Header("Effect")]
    [SerializeField] ParticleSystem psDie;
    [SerializeField] GameObject visual;
    [SerializeField] GameObject controlPanel, gameOverPanel;
    private ParticleSystem.MainModule psMain;
    private ParticleSystem.MinMaxGradient originalPsColor;

    [Header("Sound")]
    [SerializeField] AudioSource soundDamage;
    [SerializeField] AudioSource soundHealth;
    [SerializeField] AudioSource soundDie;
    [SerializeField] AudioSource soundGameOver;

    private void Awake()
    {
        instance = this;
        currHealth = health;
        UpdateHealthBar();
        gameOverPanel.SetActive(false);
        controlPanel.SetActive(true);
        psMain = psDie.main;
        originalPsColor = psMain.startColor;
    }

    public void ChangeHealth(float damage)
    {
        currHealth = Mathf.Clamp(currHealth -= damage, -1, health + 1);
        UpdateHealthBar();

        if (damage < 0)
        {
            psMain.startColor = Color.green;
            PlaySound(soundHealth, 0);
            PS(15);
        }
        else
        {
            psMain.startColor = originalPsColor;
            PlaySound(soundDamage, 0);
            PS(5);
        }

        if (currHealth <= 0) Die();
    }

    public void Die()
    {
        psMain.startColor = originalPsColor;
        PS(60);
        PlaySound(soundDie, 0);
        visual.SetActive(false);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        controlPanel.SetActive(false);
        PlaySound(soundGameOver, 1);
        Invoke("SetGameOverActive", 2);
        Destroy(gameObject, 5);
    }

    private void PS(byte burstAmount)
    {
        ParticleSystem.EmissionModule psModule = psDie.emission;
        psModule.SetBurst(0, new ParticleSystem.Burst(0, burstAmount));
        psDie.Play();
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currHealth / health;
        healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.yellow, healthSlider.normalizedValue);
    }

    private void SetGameOverActive()
    {
        gameOverPanel.SetActive(true);
        // PlaySound(soundGameOver); 
    }

    private void PlaySound(AudioSource sound, ulong delay)
    {
        if (sound.isPlaying) sound.Stop();
        sound.Play(delay * 44100);
    }
}
