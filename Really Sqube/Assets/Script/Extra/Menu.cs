using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject[] panels;
    
    [Header("Tips")]
    [SerializeField] Text tipText;
    [SerializeField] string[] tips;

    [Header("Level UI")]
    [SerializeField] Button[] levelButtons;
    [SerializeField] Scrollbar scrollBar;
    
    [Header("Collectables")]
    [SerializeField] Text txtCoin;
    [SerializeField] Text txtRealityStone;
    [SerializeField] Text txtTimeStone;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip playClip;
    [SerializeField] AudioClip buttonClip;

    private bool isFocous;
    private bool isProcessing;

    private void OnApplicationFocus(bool focus)
    {
        isFocous = focus;
    }

    private void Start()
    {
        // PlayerPrefs.SetInt("Level", 4);
        LevelsButton();
        StartCoroutine(GenerateTips());
        UpdateCollectablesTxt();
    }

    private void UpdateCollectablesTxt()
    {
        txtCoin.text = PrintAmount(PlayerPrefs.GetInt("Coin", 0));
        txtRealityStone.text = PrintAmount(PlayerPrefs.GetInt("RealityStone", 0));
        txtTimeStone.text = PrintAmount(PlayerPrefs.GetInt("TimeStone", 0));
    }

    private string PrintAmount(int amount)
    {
        string amountStr = amount.ToString();
        if (amountStr.Length > 5) return amount.ToString("0,,.##M");
        else if (amountStr.Length > 3) return amount.ToString("0,.##K");
        else return amount.ToString();
    }

    private void LevelsButton()
    {
        int currLevel = PlayerPrefs.GetInt("Level", 1);
        currLevel--;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == currLevel)
            {
                levelButtons[i].transform.localScale = (1.1f) * Vector3.one;
            }
            else if (i > currLevel)
            {
                levelButtons[i].interactable = false;
                levelButtons[i].transform.localScale = (0.8f) * Vector3.one;
            }
            else
            {
                levelButtons[i].transform.localScale = (0.8f) * Vector3.one;
            }
        }

        float scrollPos = (levelButtons.Length - (currLevel + 1));
        scrollPos = scrollPos / 10;
        // scrollBar.value = Mathf.Lerp(scrollBar.value, x, 0.1f);
        scrollBar.value = scrollPos;
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
        UpdateCollectablesTxt();
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



    public void InviteFriends()
    {
        if (!isProcessing)
        {
            StartCoroutine(IEShareText());
        }
    }

    IEnumerator IEShareText()
    {
        string shareSubject = "Sharing Really Sqube";
        string shareMessage = "What's Up Gamerz\nSharing you the link of one of best game I have played Really Sqube hope you also like it.\nAll these has been written by me & not by any stupid dum developer trust me. \n Must Download or pay the price ha ha ha... : \n" + "https://play.google.com/store/apps/details?id=com.DKSoftware.ReallySqube";
        isProcessing = true;
        if (!Application.isEditor)
        {
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Really Sqube begining of -DK-");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocous);
        isProcessing = false;
    }
}
