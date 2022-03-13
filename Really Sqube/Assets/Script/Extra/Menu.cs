using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip playClip;
    [SerializeField] AudioClip buttonClip;
    [SerializeField] Text tipText;
    [SerializeField] GameObject[] panels;
    [SerializeField] string[] tips;
    [SerializeField] Button[] levelButtons;

    private void Start()
    {
        LevelsButton();
        StartCoroutine(GenerateTips());
    }

    private void LevelsButton()
    {
        for (int i = PlayerPrefs.GetInt("Level", 1); i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }

    IEnumerator GenerateTips()
    {
        tipText.text = tips[Random.Range(0, tips.Length)];
        yield return new WaitForSeconds(5);
        tipText.CrossFadeAlpha(0, 0.5f, false); // Text fade (Gone)
        yield return new WaitForSeconds(1);
        tipText.CrossFadeAlpha(1, 0.5f, false); // Text come 

        StartCoroutine(GenerateTips());
    }



    public void PlayButton(int level)
    {
        audioSource.PlayOneShot(playClip);
        if (level == -1) level = PlayerPrefs.GetInt("Level", 1);
        SceneManager.LoadScene(level);
    }

    public void ChangePanelButton(GameObject changedPanel)
    {
        audioSource.PlayOneShot(buttonClip);
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
        changedPanel.SetActive(true);
    }

    public void LinkButton(string link)
    {
        Application.OpenURL(link);
    }

    public void QuitButton()
    {
        audioSource.PlayOneShot(buttonClip);
        Application.Quit();
    }
}
